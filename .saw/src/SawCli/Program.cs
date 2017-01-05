namespace Microsoft.Ciqs.Saw.Cli
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Ciqs.Saw.Common;
    using Microsoft.WindowsAzure.Storage;

    class Program
    {
        static int Main(string[] args)
        {
            InformationPrinter info = new InformationPrinter();   
            
            if (args.Length == 0)
            {
                info.PrintUsage();
                return 1;
            }
            
            if (args[0].Equals("help", StringComparison.InvariantCultureIgnoreCase))
            {
                if (args.Length == 2)
                {
                    info.PrintCommandDescription(args[1]);
                }
                else
                {
                    info.PrintAvailableCommands();
                }
                
                return 0;
            }
            
            var clArgsParser = new CommandLineArgumentsParser(args);
            var command = clArgsParser.Command;            
            var phaseSequence = PhaseListProvider.GetPhaseSequence(command);
            var phaseSequenceExecutor = new PhaseSequenceExecutor(phaseSequence, null);            
            phaseSequenceExecutor.Run();
            
            return 0;
        }
    }
}
