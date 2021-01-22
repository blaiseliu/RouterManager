using System;
using System.Collections.Generic;
using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RouterCommand.ParseOptions;
using RouterCommand.Processes;

namespace RouterCommand.Services
{
    public class Startup
    {
        #region DI
        private readonly ILogger<Startup> _log;
        private readonly IStatusProcess _process;

        public Startup(ILogger<Startup> log, IStatusProcess process)
        {
            _log = log;
            _process = process;
        }
        #endregion
        public void Run(string[] args)
        {
            Parser.Default.ParseArguments<StatusOption>(args)
                .WithParsed(x => _process.Process(x))                
                .WithNotParsed(HandleParseError);

            Console.WriteLine("Press any key to finish.");
            Console.ReadKey(true);
        }

        private static void HandleParseError(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }
        }
    }
}
