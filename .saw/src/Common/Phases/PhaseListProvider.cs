namespace Microsoft.Ciqs.Saw.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Ciqs.Saw.Common;

    public class PhaseListProvider
    {
        public ISawPhase Phases { get; private set; }
        
        public PhaseListProvider(string[] args)
        {
            Console.WriteLine(this.GetAllPhases());   
        }
        
        private IEnumerable<PhaseDescriptor> GetAllPhases() 
        {
            AssemblyName phasesAssembly = AssemblyName.GetAssemblyName(Constants.PhasesAssemblyPath);
            
            return Assembly.Load(phasesAssembly).GetTypes()
                .Where(t => t.IsDefined(typeof(SawPhaseAttribute), false))
                .Select(t => 
                    {
                        var phaseAttribute = t.GetCustomAttributes(typeof(SawPhaseAttribute), true)[0] as SawPhaseAttribute;
                        
                        return new PhaseDescriptor(t, phaseAttribute);
                    });
        }
        
    }
}
