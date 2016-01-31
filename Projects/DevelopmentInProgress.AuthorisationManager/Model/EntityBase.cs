using System.ComponentModel;

namespace DevelopmentInProgress.AuthorisationManager.Model
{
    public abstract class EntityBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual int Id { get; set; }
        
        public virtual string Text { get; set; }
        
        public virtual bool IsVisible { get; set; }

        public bool IsReadOnly { get; set; }

        public bool CanModify
        {
            get { return !IsReadOnly; }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            var propertyChangedHandler = PropertyChanged;
            if (propertyChangedHandler != null)
            {
                propertyChangedHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
