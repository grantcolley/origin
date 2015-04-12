using System.Threading;
using System.Windows.Input;
using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.Messages;
using DevelopmentInProgress.Origin.ViewModel;
using System;
using System.Collections.Generic;

namespace DevelopmentInProgress.ExampleModule.ViewModel
{
    public class ExampleModalViewModel : ModalViewModel
    {
        private string text = "Modify the text to see the dirty indicator in the document header...";

        public ExampleModalViewModel(ViewModelContext viewModelContext)
            : base(viewModelContext)
        {
            ShowMessageCommand = new ViewModelCommand(ShowMessage);
            ClearMessagesCommand = new ViewModelCommand(ClearMessages);
            ShowMessageBoxCommand = new ViewModelCommand(OpenMessageBox);
            ResetStatusCommand = new ViewModelCommand(ResetDocumentStatus);
            ThrowExceptionCommand = new ViewModelCommand(ThrowException);
        }

        public ICommand ShowMessageCommand { get; set; }
        public ICommand ClearMessagesCommand { get; set; }
        public ICommand ShowMessageBoxCommand { get; set; }
        public ICommand ResetStatusCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ThrowExceptionCommand { get; set; }

        public string ModalParameter { get; set; }
        public string MessageType { get; set; }
        public string MessageText { get; set; }
        public string MessageBoxButton { get; set; }
        public string MessageIcon { get; set; }
        public string MessageBoxText { get; set; }

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

        protected override ProcessAsyncResult OnPublishedAsync(Dictionary<string, object> parameters)
        {
            if (parameters != null
                && parameters["ModalParameter"] != null)
            {
                ModalParameter = parameters["ModalParameter"].ToString();
                OnPropertyChanged(String.Empty);
            }

            Thread.Sleep(1000);

            return new ProcessAsyncResult();
        }

        protected override void OnPublishedCompleted(ProcessAsyncResult processAsyncResult)
        {
            base.OnPublishedCompleted(processAsyncResult);
        }

        protected override ProcessAsyncResult SaveDocumentAsync()
        {
            Thread.Sleep(1000);

            return new ProcessAsyncResult();
        }

        private void ShowMessage(object parameter)
        {
            var message = new Message() { Text = MessageText };
            SetMessageImage(message, MessageType);
            ShowMessage(message, true);
        }

        private void ClearMessages(object parameter)
        {
            ClearMessages();
        }

        private void OpenMessageBox(object parameter)
        {
            var message = new Message() { Text = MessageBoxText, Title = String.Format("Show {0}", MessageIcon) };
            SetMessageImage(message, MessageIcon);

            var messageBoxSettings = new MessageBoxSettings() { Message = message };

            switch (MessageBoxButton)
            {
                case "Ok":
                    messageBoxSettings.MessageBoxButtons = MessageBoxButtonsEnum.Ok;
                    break;
                case "Ok Cancel":
                    messageBoxSettings.MessageBoxButtons = MessageBoxButtonsEnum.OkCancel;
                    break;
                case "Yes No":
                    messageBoxSettings.MessageBoxButtons = MessageBoxButtonsEnum.YesNo;
                    break;
                case "Yes No Cancel":
                    messageBoxSettings.MessageBoxButtons = MessageBoxButtonsEnum.YesNoCancel;
                    break;
            }

            var result = ShowMessageBox(messageBoxSettings);
        }

        private void SetMessageImage(Message message, string image)
        {
            switch (image)
            {
                case "Info":
                    message.MessageType = MessageTypeEnum.Info;
                    break;
                case "Warn":
                    message.MessageType = MessageTypeEnum.Warn;
                    break;
                case "Error":
                    message.MessageType = MessageTypeEnum.Error;
                    break;
                case "Question":
                    message.MessageType = MessageTypeEnum.Question;
                    break;
            }
        }

        private void ResetDocumentStatus(object parameter)
        {
            ResetStatus();
        }

        private void ThrowException(object parameter)
        {
            int zero = 0;
            int result = 1 / zero;
        }
    }
}
