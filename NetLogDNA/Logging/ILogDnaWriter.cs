using System;
using System.Threading.Tasks;
using NetLogDNA.LogDnaApi.Dto;

namespace NetLogDNA.Logging
{
    public interface ILogDnaWriter : IDisposable
    {
        void Start();

        Task Stop();

        ILogDnaWriter AddLine(LogLine logLine);
    }
}