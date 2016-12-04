using System.Threading;
using System.Threading.Tasks;
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

        protected async override void OnPublished(object data)
        {
            IsBusy = true;

            await Task.Run(() => Thread.Sleep(1000));

            ResetStatus();
            OnPropertyChanged("");
        }

        protected async override void SaveDocument()
        {
            IsBusy = true;

            await Task.Run(() => Thread.Sleep(1000));

            ResetStatus();
            OnPropertyChanged("");
        }

        private void ResetDocumentStatus(object parameter)
        {
            ResetStatus();
        }
    }
}
