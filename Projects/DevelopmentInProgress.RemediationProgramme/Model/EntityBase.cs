using System;
using System.ComponentModel;
using DevelopmentInProgress.DipState;

namespace DevelopmentInProgress.RemediationProgramme.Model
{
    public abstract class EntityBase : DipState.DipState, INotifyPropertyChanged
    {
        protected EntityBase(int id = 0, string name = "", bool initialiseWithParent = false,
            bool canCompleteParent = false, DipStateType type = DipStateType.Standard,
            DipStateStatus status = DipStateStatus.Uninitialised, Predicate<DipState.DipState> canComplete = null)
            : base(id, name, initialiseWithParent, canCompleteParent, type, status, canComplete)
        {            
        }

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
