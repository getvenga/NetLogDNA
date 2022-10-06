using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetLogDNA.LogDnaApi;
using NetLogDNA.LogDnaApi.Dto;
using NetLogDNA.LogDnaApi.Requests;
using NetLogDNA.Utils;

namespace NetLogDNA.Logging
{
    public class LogDnaWriter : ILogDnaWriter
    {
        private readonly object _linesLock;
        
        private readonly ILogDnaApi _logDnaApi;

        private readonly IDnsInfoProvider _dnsInfoProvider;
        
        private readonly LinkedList<LogLine> _logLines;

        private bool _isDisposed;

        private bool _isRunning;
        
        private CancellationTokenSource _cancellationTokenSource;

        private Task _loop;

        public bool Verbose { get; set; } = false;

        internal LogDnaWriter(ILogDnaApiFactory logDnaApiFactory, IDnsInfoProvider dnsInfoProvider)
        {
            _isDisposed = false;
            _isRunning = false;
            _logLines = new LinkedList<LogLine>();
            _linesLock = new object();
            
            _dnsInfoProvider = dnsInfoProvider;
            _logDnaApi = logDnaApiFactory.Create();
        }

        public ILogDnaWriter AddLine(LogLine logLine)
        {
            lock (_linesLock)
            {
                _logLines.AddLast(logLine);
                
                if (Verbose)
                    Trace.WriteLine($"Queue is containing {_logLines.Count()} log lines");
            }


            return this;
        }

        public async Task Stop()
        {
            if (_isDisposed || !_isRunning)
                return;

            _cancellationTokenSource?.Cancel();

            await _loop;

            _isRunning = false;
        }

        public void Start()
        {
            if (_isDisposed || _isRunning)
                return;

            _isRunning = true;
            _cancellationTokenSource = new CancellationTokenSource();

            _loop = Task.Run(Execute, CancellationToken.None);
        }

        private async Task Execute()
        {
            if (Verbose)
                Trace.WriteLine("Execution of writer loop started. Waiting for log lines...");
            
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                // wait for processing
                if (Verbose)
                    Trace.WriteLine($"Loop waiting for {LogDnaConfig.WriterLoopInterval} ms");
                
                await Task.Delay(LogDnaConfig.WriterLoopInterval);

                if (!HasLines())
                {
                    if (Verbose)
                        Trace.WriteLine($"No lines this time...  see you in {LogDnaConfig.WriterLoopInterval} ms");
                    
                    continue;
                }
                
                LogLineBatch logLinesBatch;
                
                lock (_linesLock)
                {
                    var currentLogLines = new LogLine[_logLines.Count];
                    _logLines.CopyTo(currentLogLines, 0);
                    _logLines.Clear();
                    
                    logLinesBatch = new LogLineBatch(currentLogLines);
                }
                
                if (Verbose)
                    Trace.WriteLine($"Prepared batch with {logLinesBatch.Lines.Count()} log lines");

                await SendBatchWithRetry(logLinesBatch);
            }
            
            if (Verbose)
                Trace.WriteLine("Stopping execution of writer loop. Good bye!");
        }

        private async Task SendBatchWithRetry(LogLineBatch logLineBatch)
        {
            var tries = 1;
            var successful = false;

            do
            {
                var request = new IngestRequest()
                {
                    HostName = _dnsInfoProvider.GetHostName()
                };
                
                var response = await _logDnaApi.Ingest(request, LogDnaConfig.AuthorizationHeader, logLineBatch);

                successful = response.IsSuccessStatusCode;

                if (!successful)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    
                    await Console.Error.WriteAsync(
                        $"Failed to log batch of {logLineBatch.Lines.Count()} log lines: {response.StatusCode} - {errorMessage}"
                    );
                }
                else if (Verbose)
                {
                    Trace.WriteLine(
                        $"Successfully sent batch of {logLineBatch.Lines.Count()} on attempt {tries}: : {response.StatusCode}"
                    );
                }

            } while (!successful && tries++ <= LogDnaConfig.WriterMaxRetries);

            if (!successful)
            {
                await Console.Error.WriteAsync(
                    $"Failed to log batch of {logLineBatch.Lines.Count()} log lines after {LogDnaConfig.WriterMaxRetries} attempts"
                );
            }
        }

        private bool HasLines()
        {
            lock (_linesLock)
                return _logLines.Any();
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            _logDnaApi.Dispose();

            Stop().GetAwaiter().GetResult();
        }
    }
}