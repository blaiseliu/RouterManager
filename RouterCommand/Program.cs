using System;
using System.Collections.Generic;
using CommandLine;
using RouterCommand.ParseOptions;
using RouterCommand.Processes;
using Serilog;

namespace RouterCommand
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<StatusOption>(args)
                .WithParsed(x=>new StatusProcess().Process(x))
                .WithNotParsed(HandleParseError);
            
            Console.WriteLine("Press any key to finish.");
            Console.ReadKey(true);
        }
        private static void HandleParseError(IEnumerable<Error> errors)
        {

            Log.Error("Cannot Parse");
            foreach (var error in errors)
            {
                Log.Error(error.ToString());
            }
        }
    }
}
