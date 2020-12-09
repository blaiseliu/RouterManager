using System;
using RouterCommand.ParseOptions;
using Serilog;
using Serilog.Core;

namespace RouterCommand.Processes
{
    public abstract class ProcessBase<T> : IProcess<T>
        where T : ParseOptionBase
    {
        public abstract void Process(T options);
        protected Logger Logger =  new LoggerConfiguration()
            .WriteTo.File($"D:\\Logs\\NetGear{DateTime.Now:yyyyMMdd.HHmmss}.log")
            .CreateLogger();
    }
}