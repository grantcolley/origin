using System;
using System.ComponentModel;
using System.Threading.Tasks;
using DevelopmentInProgress.DipState;

namespace DevelopmentInProgress.ExampleModule.Model
{
    public abstract class EntityBase : State, INotifyPropertyChanged
    {
        private bool inProgress;

        protected EntityBase(int id = 0, string name = "", StateType type = StateType.Standard,
            StateStatus status = StateStatus.Uninitialised)
            : base(id, name, type, status)
        {            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool InProgress
        {
            get { return inProgress; }
            set
            {
                inProgress = value;
                OnPropertyChanged("InProgress");
                OnPropertyChanged("CanComplete");
            }
        }

        public bool CanComplete
        {
            get
            {
                if (InProgress
                    || Status.Equals(StateStatus.Completed))
                {
                    return false;
                }

                return true;
            }
        }

        public bool CanModify
        {
            get { return !IsReadOnly; }
        }

        public bool IsReadOnly
        {
            get { return Status.Equals(StateStatus.Completed); }
        }

        public virtual async Task RefreshAsync(State state)
        {
            OnPropertyChanged(String.Empty);
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
