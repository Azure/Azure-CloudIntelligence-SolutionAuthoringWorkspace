namespace Microsoft.Ciqs.Saw.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Ciqs.Saw.Common;
    using Microsoft.Ciqs.Saw.Common.Utilities;

    public static class PhaseListProvider
    {
        private static Lazy<IList<PhaseDescriptor>> phases = new Lazy<IList<PhaseDescriptor>>(
            () => PhaseListProvider.GetAllPhases());
        
        public static IList<PhaseDescriptor> AllPhases
        {
            get 
            {
                return PhaseListProvider.phases.Value;
            }
        }
        
        public static IList<PhaseDescriptor> GetPhaseSequence(string name)
        {
            var result = PhaseListProvider.AllPhases.Where(ph => ph.Name.Equals(name)).ToList();
            if (result.Count == 0)
            {
                throw new SawPhaseException($"Unknown phase `{name}`");
            }
            
            return result;
        }
        
        private static IList<PhaseDescriptor> GetAllPhases() 
        {
            AssemblyName phasesAssembly = AssemblyName.GetAssemblyName(Constants.PhasesAssemblyPath);
            
            var phaseDictionary = Assembly.Load(phasesAssembly).GetTypes()
                .Where(t => t.IsDefined(typeof(PhaseAttribute), false))
                .Select(t => 
                    {
                        var phaseAttribute = t.GetCustomAttributes(typeof(PhaseAttribute), false)[0] as PhaseAttribute;
                        
                        var parameters = t.GetProperties().Where(p => Attribute.IsDefined(p, typeof(ParameterAttribute))).Select(p => 
                            new ParameterDescriptor(p, p.GetCustomAttributes(typeof(ParameterAttribute)).First() as ParameterAttribute)).ToArray();
                        
                        return new PhaseDescriptor(t, phaseAttribute, parameters);
                    }).ToDictionary(p => p.Name, p => p).TopologicalOrder();
            
        }
    }
}
