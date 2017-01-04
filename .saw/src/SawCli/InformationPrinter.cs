namespace Microsoft.Ciqs.Saw.Cli
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Ciqs.Saw.Common;
    
    class InformationPrinter
    {
        public void PrintUsage()
        {            
            Console.WriteLine("Try `saw help`.");
        }
        
        public void PrintAvailableCommands()
        {
            Console.WriteLine("Available SAW commands:\n");
            foreach (var phase in PhaseListProvider.AllPhases)
            {
                Console.WriteLine($"{phase.Name}\n\t- {phase.Description}");
            }
            
            Console.WriteLine("\nFor detailed information, try `saw help <command>`.");
        }
        
        public void PrintInvalidCommandMessage(string command)
        {
            Console.Write($"`{command}` is unrecognized. ");
            this.PrintUsage();
        }
        
        public void PrintCommandDescription(string command)
        {
            var phaseSequence = PhaseListProvider.GetPhaseSequence(command);
            
            var parameters = new Dictionary<string, ParameterDescriptor>();
            
            foreach (var phase in phaseSequence)
            {
                phase.Parameters.ToList().ForEach(p => parameters.Add(p.Name, p));
            }
            
            Console.WriteLine($"Parameters for the `{command}` command: \n");
            
            foreach (var kvp in parameters)
            {
                Console.WriteLine($"-{kvp.Key}");
                Console.WriteLine($"\t{kvp.Value.Description}");
            }
        }        
    }   
}