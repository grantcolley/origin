using System.ComponentModel;
using DevelopmentInProgress.DipState;

namespace DevelopmentInProgress.RemediationProgramme.Model
{
    public class EntityBase : DipState.DipState, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool CanModify
        {
            get { return !IsReadOnly; }
        }

        public bool IsReadOnly
        {
            get { return Status.Equals(DipStateStatus.Completed); }
        }

        public void OnPropertyChanged(string propertyName)
        {
            var propertyChangedHandler = PropertyChanged;
            if (propertyChangedHandler != null)
            {
                propertyChangedHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
