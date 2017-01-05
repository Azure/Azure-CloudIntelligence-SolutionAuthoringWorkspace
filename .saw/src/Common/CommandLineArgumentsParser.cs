namespace Microsoft.Ciqs.Saw.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class CommandLineArgumentsParser
    {
        private readonly Regex keyRegex = new Regex(@"^-([A-Z]+)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        
        private IDictionary<string, string> parameters = new Dictionary<string, string>();

        public string Command { get; private set; }
        
        public IDictionary<string, string> Parameters
        {
            get
            {
                return new Dictionary<string, string>(this.parameters);
            }            
        } 
        
        public CommandLineArgumentsParser(string[] args)
        {
            this.Parse(args);
        }       
        
        private void Parse(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                return;
            }
                        
            this.Command = args[0];
                        
            string currentKey = null;

            for (int i = 1; i < args.Length; i++)
            {
                var currentArgument = args[i];
                Match match = this.keyRegex.Match(currentArgument);   
                if (match.Success)
                {
                    currentKey = match.Groups[1].Value.ToLower();
                    if (!parameters.ContainsKey(currentKey))
                    {
                        parameters.Add(currentKey, string.Empty);
                    }
                }
                else 
                {
                    if (currentKey == null)
                    {
                        throw new SawException($"Unrecognized parameter: {currentArgument}");
                    }
                    else
                    {
                        parameters[currentKey] = parameters[currentKey] + " " + currentArgument;
                    }
                }
            }
        }
    }
}
