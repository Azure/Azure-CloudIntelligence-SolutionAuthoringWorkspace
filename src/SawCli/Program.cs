namespace Microsoft.Ciqs.Saw.Cli
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Ciqs.Saw.Common;
    using Microsoft.WindowsAzure.Storage;
    using System.Configuration;
    using System.Diagnostics;

    class Program
    {
        static int Main(string[] args)
        {            
            if (args.Length == 0)
            {
                InformationPrinter info = new InformationPrinter(Program.GetSettings());

                info.PrintUsage();
                return 1;
            }

            if (args[0].Equals("shell"))
            {
                var info = new ProcessStartInfo("cmd", "/k saw help");
                info.UseShellExecute = false;

                var proc = Process.Start(info);
                proc.WaitForExit();

                return 0;
            }

            if (args[0].Equals("help", StringComparison.InvariantCultureIgnoreCase))
            {
                InformationPrinter info = new InformationPrinter(Program.GetSettings());

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

            if ("configure".Equals(command, StringComparison.InvariantCultureIgnoreCase))
            {
                clArgsParser.Parameters.ToList().ForEach(p => SawCli.Properties.Settings.Default[p.Key] = p.Value);
                SawCli.Properties.Settings.Default.Save();

                InformationPrinter info = new InformationPrinter(Program.GetSettings());
                info.PrintNewSettings();

                return 0;
            }

            var phaseSequence = PhaseListProvider.GetPhaseSequence(command);

            var parameterPool = Program.GetSettings();
            
            clArgsParser.Parameters.ToList().ForEach(p => parameterPool[p.Key] = p.Value);
            
            var phaseSequenceExecutor = new PhaseSequenceExecutor(phaseSequence, parameterPool);            
            
            phaseSequenceExecutor.Run();
            
            return 0;
        }

        private static IDictionary<string, string> GetSettings()
        {
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var property in SawCli.Properties.Settings.Default.Properties)
            {
                var settingsProperty = (property as SettingsProperty);
                var propertyName = settingsProperty.Name;
                result.Add(propertyName, SawCli.Properties.Settings.Default[settingsProperty.Name].ToString());
            }

            return result;
        }
    }
}
