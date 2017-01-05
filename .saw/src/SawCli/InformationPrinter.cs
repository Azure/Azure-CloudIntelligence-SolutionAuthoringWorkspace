namespace Microsoft.Ciqs.Saw.Cli
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Microsoft.Ciqs.Saw.Common;
    
    class InformationPrinter
    {
        private const string accountKeyReplacementString = "AccountKey=***;";
        
        private const string genericReplacementString = "***secure***";
        
        private static readonly Regex accountKeyRegex = new Regex(@"AccountKey=([^;]+);?", RegexOptions.IgnoreCase | RegexOptions.Compiled);        
        
        private IDictionary<string, string> parameterPool;
        
        public InformationPrinter(IDictionary<string, string> parameterPool)
        {
            this.parameterPool = parameterPool;
        }
        
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
            
            var parameterDescriptions = new Dictionary<string, string>();
            var requiredParameters = new HashSet<string>();
            var secureParameters = new HashSet<string>();
            
            foreach (var phase in phaseSequence)
            {                
                phase.Parameters.ToList().ForEach(p => {                                        
                    if (p.Required)
                    {
                        requiredParameters.Add(p.Name);
                    }
                    
                    if (p.Secure)
                    {
                        secureParameters.Add(p.Name);
                    }
                    
                    if (p.Description != null)
                    {
                        parameterDescriptions.Add(p.Name, p.Description);
                    }
                    
                    
                });
            }
            
            Console.WriteLine($"Parameters for the `{command}` command: \n");
            
            foreach (var kvp in parameterDescriptions)
            {
                var name = kvp.Key;
                Console.WriteLine($"-{name}");
                Console.Write($"\t{kvp.Value} ");
                
                if (requiredParameters.Contains(name) && !this.parameterPool.ContainsKey(name))
                {
                    Console.WriteLine("(required)");
                }
                else
                {
                    if (this.parameterPool.ContainsKey(name))
                    {
                        var defaultValue = parameterPool[name];
                        
                        if (secureParameters.Contains(name))
                        {
                            defaultValue = this.SanitizeSecureString(defaultValue);
                        }
                        
                        Console.WriteLine($"[{defaultValue}]");
                    }
                    else
                    {
                        Console.WriteLine("(optional)");
                    }
                }
            }
        }
        
        private string SanitizeSecureString(string value)
        {
            Match match = InformationPrinter.accountKeyRegex.Match(value);   
            if (match.Success)
            {
                value = accountKeyRegex.Replace(value, InformationPrinter.accountKeyReplacementString);
            }
            else
            {
                value = InformationPrinter.genericReplacementString;
            }
            
            return value;
        } 
    }   
}