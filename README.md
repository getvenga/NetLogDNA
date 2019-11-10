# NetLogDNA

This is an unoffical library to write logs to [LogDNA](https://logdna.com/). It uses an internal worker Task that sends batches of logs to the DNA. An extra library includes a Target that can be used with [NLog](https://nlog-project.org).

## Usage with NLog

The target needs to be defined just like any other target. Since it is not part of NLog itself the assembly needs ot be referenced in the XML to be recognised:

```XML
<extensions>
    <add assembly="NetLogDNA.NLog"/>
</extensions>

<targets>
    <target name="logdna_target" 
        xsi:type="LogDNA" 
        AppName="[Name of your app as it is supposed to be shown in LogDNA]" 
        ApiKey="[Enter your LogDNA API Key here]" 
        layout="${longdate} ${callsite} ${level} ${message}" />
</targets>

<rules>
    <logger minlevel="Trace" 
        name="*" 
        writeTo="logdna_target" />
</rules>
```

#### Configuration

Certain configurations can be set via the static `LogDnaConfig` class:

```CSharp
LogDnaConfig.WriterMaxRetries = 3; // internal retry if API connection fails. default: 5
LogDnaConfig.WriterLoopInterval = 200; // Interval in ms in which all collected logs will be sent to the API. default: 100
LogDnaConfig.AppName = "MyApp"; // Override the AppNAme set in the XML
LogDnaConfig.ApiKey = "..."; // Set the API key programacially
```

## Collaboration

This repository is maintained by `Daniel Lerps`. Further development is welcome. Please use pull-requests for new features and extensions.

## To-do

 - CI with publishing NuGet package
 - More unit tests
 - Extending NLog Target with async, flushing and other special logging features
 - Add simple logger to make logging without NLog easier