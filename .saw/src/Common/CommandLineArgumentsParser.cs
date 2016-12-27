namespace Microsoft.Ciqs.Saw.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class CommandLineArgumentsParser
    {
        public string Command { get; private set; }
        
        private IDictionary<string, string> parameters = new Dictionary<string, string>();
        
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

            string pattern = @"^-([A-Z]+)$";
            Regex keyRegex = new Regex(pattern, RegexOptions.IgnoreCase);

            for (int i = 1; i < args.Length; i++)
            {
                var currentArgument = args[i];
                Match match = keyRegex.Match(currentArgument);   
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
