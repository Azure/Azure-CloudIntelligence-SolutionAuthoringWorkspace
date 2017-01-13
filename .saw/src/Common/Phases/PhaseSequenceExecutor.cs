namespace Microsoft.Ciqs.Saw.Common
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    
    public class PhaseSequenceExecutor
    {
        private IEnumerable<IPhase> phaseInstances;
        
        public PhaseSequenceExecutor(IEnumerable<PhaseDescriptor> phaseSequence, IDictionary<string, string> parameterPool)
        {
            this.phaseInstances = this.GetPhaseInstances(phaseSequence, parameterPool);
        }
        
        public void Run()
        {
            foreach(var phase in this.phaseInstances)
            {
                phase.Run();
            }
        }
        
        private IEnumerable<IPhase> GetPhaseInstances(IEnumerable<PhaseDescriptor> phaseSequence, IDictionary<string, string> parameterPool)
        {
            return phaseSequence.Select(pd => this.CreatePhaseInstance(pd, parameterPool));         
        }
        
        private IPhase CreatePhaseInstance(PhaseDescriptor phaseDescriptor, IDictionary<string, string> parameterPool)
        {
            IPhase instance = Activator.CreateInstance(phaseDescriptor.Type) as IPhase;
            
            foreach (var parameter in phaseDescriptor.Parameters)
            {
                if (parameterPool.ContainsKey(parameter.Name))
                {
                    var value = parameterPool[parameter.Name];
                    var propertyInfo = parameter.PropertyInfo;
                    
                    if (propertyInfo.PropertyType == typeof(string))
                    {
                        propertyInfo.SetValue(instance, value, null);
                    }
                    else if (propertyInfo.PropertyType == typeof(string[]))
                    {
                        propertyInfo.SetValue(instance, value.Split( new char[] {' ', ','}, StringSplitOptions.RemoveEmptyEntries), null);
                    }
                    else
                    {
                        throw new SawPhaseException($"Unable to inject the value for parameter `{parameter.Name}`of phase `{phaseDescriptor.Name}`");
                    }
                }
                else
                {
                    if (parameter.Required)
                    {
                        throw new SawPhaseException($"Missing required parameter `{parameter.Name}`");
                    }
                }
            }
            
            return instance;
        }
    }
}