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
        private static Lazy<IEnumerable<PhaseDescriptor>> phases = new Lazy<IEnumerable<PhaseDescriptor>>(
            () => PhaseListProvider.GetAllPhases());
        
        public static IEnumerable<PhaseDescriptor> AllPhases
        {
            get 
            {
                return PhaseListProvider.phases.Value;
            }
        }
        
        public static IEnumerable<PhaseDescriptor> GetPhaseSequence(string name)
        {
            var phasesInOrder = PhaseListProvider.AllPhases.SkipWhile(ph => !ph.Name.Equals(name)).ToList();
            if (phasesInOrder.Count == 0)
            {
                throw new SawPhaseException($"Unknown phase `{name}`");
            }

            var dependencySet = new HashSet<string>();
            dependencySet.Add(name);
            
            return phasesInOrder.Where(ph => {
                if (!dependencySet.Contains(ph.Name))
                {
                    return false;
                }
                
                dependencySet.UnionWith(ph.Dependencies ?? Enumerable.Empty<string>());
                
                return true;
            }).Reverse();
        }
        
        private static IEnumerable<PhaseDescriptor> GetAllPhases() 
        {
            AssemblyName phasesAssembly = AssemblyName.GetAssemblyName(Constants.PhasesAssemblyPath);
            
            return Assembly.Load(phasesAssembly).GetTypes()
                .Where(t => t.IsDefined(typeof(PhaseAttribute), false))
                .Select(t => 
                    {
                        var phaseAttribute = t.GetCustomAttributes(typeof(PhaseAttribute), false)[0] as PhaseAttribute;
                        
                        var parameters = t.GetProperties().Where(p => Attribute.IsDefined(p, typeof(ParameterAttribute))).Select(p => 
                            new ParameterDescriptor(p, p.GetCustomAttributes(typeof(ParameterAttribute)).First() as ParameterAttribute)).ToArray();
                        
                        return new PhaseDescriptor(t, phaseAttribute, parameters);
                    }).TopologicalOrder(pd => pd.Name, pd => pd.Dependencies).Reverse();
        }
    }
}
