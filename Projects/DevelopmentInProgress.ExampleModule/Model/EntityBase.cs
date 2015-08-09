using System;
using System.ComponentModel;
using DevelopmentInProgress.DipState;

namespace DevelopmentInProgress.ExampleModule.Model
{
    public abstract class EntityBase : State, INotifyPropertyChanged
    {
        private bool inProgress;

        protected EntityBase(int id = 0, string name = "", bool initialiseWithParent = false,
            bool canCompleteParent = false, StateType type = StateType.Standard,
            StateStatus status = StateStatus.Uninitialise)
            : base(id, name, initialiseWithParent, canCompleteParent, type, status)
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
                    || Status.Equals(StateStatus.Complete))
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
            get { return Status.Equals(StateStatus.Complete); }
        }

        public void Refresh()
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
