using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DevelopmentInProgress.DipState;

namespace DevelopmentInProgress.RemediationProgramme.Model
{
    public class Customer : INotifyPropertyChanged
    {
        private List<DipState.DipState> remediationWorkflow;

        public event PropertyChangedEventHandler PropertyChanged;

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

        public void OnPropertyChanged(string propertyName)
        {
            var propertyChangedHandler = PropertyChanged;
            if (propertyChangedHandler != null)
            {
                propertyChangedHandler(this, new PropertyChangedEventArgs(propertyName));
                RemediationWorkflow.ForEach(s => ((EntityBase) s).OnPropertyChanged("Status"));
            }
        }
    }
}