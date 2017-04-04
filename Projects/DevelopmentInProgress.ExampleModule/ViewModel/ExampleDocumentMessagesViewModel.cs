using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.ViewModel;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using msg = DevelopmentInProgress.WPFControls.Messaging;

namespace DevelopmentInProgress.ExampleModule.ViewModel
{
    public class ExampleDocumentMessagesViewModel : DocumentViewModel
    {
        public ExampleDocumentMessagesViewModel(ViewModelContext viewModelContext)
            : base(viewModelContext)
        {            
            ShowMessageCommand = new ViewModelCommand(ShowMessage);
            ClearMessagesCommand = new ViewModelCommand(ClearMessages);
            ShowMessageBoxCommand = new ViewModelCommand(OpenMessageBox);
            ThrowExceptionCommand = new ViewModelCommand(ThrowException);
        }

        public ICommand ShowMessageCommand { get; set; }
        public ICommand ClearMessagesCommand { get; set; }
        public ICommand ShowMessageBoxCommand { get; set; }
        public ICommand ThrowExceptionCommand { get; set; }

        public string MessageType { get; set; }
        public string MessageText { get; set; }
        public string MessageBoxButton { get; set; }
        public string MessageIcon { get; set; }
        public string MessageBoxText { get; set; }

        public string[] MessageTypes
        {
            get
            {
                return new[] { "Info", "Warn", "Error", "Question" };
            }
        }

        public string[] MessageBoxButtons
        {
            get
            {
                return new[] { "Ok", "Ok Cancel", "Yes No", "Yes No Cancel" };
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
        
        private void ShowMessage(object parameter)
        {
            var message = new msg.Message() { Text = MessageText };
            SetMessageImage(message, MessageType);
            ShowMessage(message, true);
        }

        private void ClearMessages(object parameter)
        {
            ClearMessages();
        }

        private void OpenMessageBox(object parameter)
        {
            var message = new msg.MessageBoxSettings() { Text = MessageBoxText, Title = String.Format("Show {0}", MessageIcon) };
            SetMessageImage(message, MessageIcon);

            switch (MessageBoxButton)
            {
                case "Ok":
                    message.MessageBoxButtons = msg.MessageBoxButtons.Ok;
                    break;
                case "Ok Cancel":
                    message.MessageBoxButtons = msg.MessageBoxButtons.OkCancel;
                    break;
                case "Yes No":
                    message.MessageBoxButtons = msg.MessageBoxButtons.YesNo;
                    break;
                case "Yes No Cancel":
                    message.MessageBoxButtons = msg.MessageBoxButtons.YesNoCancel;
                    break;
            }

            var result = ShowMessageBox(message);
        }

        private void SetMessageImage(msg.Message message, string image)
        {
            switch (image)
            {
                case "Info":
                    message.MessageType = msg.MessageType.Info;
                    break;
                case "Warn":
                    message.MessageType = msg.MessageType.Warn;
                    break;
                case "Error":
                    message.MessageType = msg.MessageType.Error;
                    break;
                case "Question":
                    message.MessageType = msg.MessageType.Question;
                    break;
            }
        }

        private void ThrowException(object parameter)
        {
            int zero = 0;
            int result = 1 / zero;
        }
    }
}
