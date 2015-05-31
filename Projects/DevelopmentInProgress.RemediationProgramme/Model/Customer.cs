using System.Collections.Generic;
using System.Linq;
using DevelopmentInProgress.DipState;

namespace DevelopmentInProgress.RemediationProgramme.Model
{
    public class Customer
    {
        private List<DipState.DipState> remediationWorkflow;

        public string Name { get; set; }
        public string SortCode { get; set; }
        public string AccountNumber { get; set; }
        public string Address { get; set; }

        public List<DipState.DipState> RemediationWorkflow
        {
            get
            {
                return remediationWorkflow.Where(s => !s.Status.Equals(DipStateStatus.Uninitialised)
                                                      && s.Type.Equals(DipStateType.Standard)).ToList();
            }
            set { remediationWorkflow = value; }
        }
    }
}