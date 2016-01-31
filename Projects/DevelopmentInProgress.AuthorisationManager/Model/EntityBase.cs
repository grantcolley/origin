using System.ComponentModel;

namespace DevelopmentInProgress.AuthorisationManager.Model
{
    public abstract class EntityBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int Id { get; set; }

        public string Text { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public bool IsVisible { get; set; }

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
