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
            
            return instance;
        }
    }
}