using System;
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

        protected override ProcessAsyncResult OnPublishedAsync(object data)
        {
            Thread.Sleep(1000);

            return new ProcessAsyncResult();
        }

        protected override ProcessAsyncResult SaveDocumentAsync()
        {
            Thread.Sleep(1000);

            return new ProcessAsyncResult();
        }

        private void ResetDocumentStatus(object parameter)
        {
            ResetStatus();
        }
    }
}
