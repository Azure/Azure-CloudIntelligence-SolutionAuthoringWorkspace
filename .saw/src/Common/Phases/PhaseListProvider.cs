namespace Microsoft.Ciqs.Saw.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Ciqs.Saw.Common;

    public static class PhaseListProvider
    {
        private static Lazy<IList<PhaseDescriptor>> phases = new Lazy<IList<PhaseDescriptor>>(
            () => PhaseListProvider.GetAllPhases());
        
        public static IList<PhaseDescriptor> Phases
        {
            get 
            {
                return PhaseListProvider.phases.Value;
            }
        }
        
        private static IList<PhaseDescriptor> GetAllPhases() 
        {
            AssemblyName phasesAssembly = AssemblyName.GetAssemblyName(Constants.PhasesAssemblyPath);
            
            return Assembly.Load(phasesAssembly).GetTypes()
                .Where(t => t.IsDefined(typeof(PhaseAttribute), false))
                .Select(t => 
                    {
                        var phaseAttribute = t.GetCustomAttributes(typeof(PhaseAttribute), false)[0] as PhaseAttribute;
                        
                        var parameters = t.GetProperties().Where(p => Attribute.IsDefined(p, typeof(ParameterAttribute))).Select(p => 
                            new ParameterDescriptor(p, p.GetCustomAttributes(typeof(ParameterAttribute)).First() as ParameterAttribute));
                        
                        return new PhaseDescriptor(t, phaseAttribute);
                    }).ToList();
        }
    }
}
