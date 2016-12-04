using System.Threading;
using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.ViewModel;
using System.Windows.Input;

namespace DevelopmentInProgress.ExampleModule.ViewModel
{
    public class ExampleDocumentViewModel : DocumentViewModel
    {
        private string text = "Modify the text to see the dirty indicator in the document header...";

        public ExampleDocumentViewModel(ViewModelContext viewModelContext)
            : base(viewModelContext)
        {            
            ResetStatusCommand = new ViewModelCommand(ResetDocumentStatus);            
        }

        public ICommand ResetStatusCommand { get; set; }

        public string Text
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    text = value;
                    OnPropertyChanged("Text", true);
                }
            }
        }

        protected override void OnPublished(object data)
        {
            Thread.Sleep(1000);
        }

        protected override void SaveDocument()
        {
            Thread.Sleep(1000);
        }

        private void ResetDocumentStatus(object parameter)
        {
            ResetStatus();
        }
    }
}
