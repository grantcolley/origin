//-----------------------------------------------------------------------
// <copyright file="MessageBoxViewModel.cs" company="Development In Progress Ltd">
//     Copyright © 2012. All rights reserved.
// </copyright>
// <author>Grant Colley</author>
//-----------------------------------------------------------------------

using System;
using DevelopmentInProgress.Origin.Converters;

namespace DevelopmentInProgress.Origin.Messages
{
    /// <summary>
    /// The view model for the <see cref="MessageBoxView"/>.
    /// </summary>
    public class MessageBoxViewModel
    {
        private const string OK = "Ok";
        private const string CANCEL = "Cancel";
        private const string YES = "Yes";
        private const string NO = "No";

        private readonly Message message;
        
        private bool isClosing;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBoxViewModel"/>.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="messageBoxButtons">The buttons to display.</param>
        /// <param name="copyToClipboardEnabled">Indicates whether to enable copy to clipboard.</param>
        public MessageBoxViewModel(Message message, MessageBoxButtonsEnum messageBoxButtons, bool copyToClipboardEnabled)
        {
            this.message = message;
            CopyToClipboardEnabled = copyToClipboardEnabled;

            switch (messageBoxButtons)
            {
                case MessageBoxButtonsEnum.Ok:
                    ButtonLeftVisible = false;
                    ButtonLeftText = String.Empty;
                    ButtonCentreVisible = true;
                    ButtonCentreText = OK;
                    ButtonRightVisible = false;
                    ButtonRightText = String.Empty;
                    break;
                case MessageBoxButtonsEnum.OkCancel:
                    ButtonLeftVisible = true;
                    ButtonLeftText = OK;
                    ButtonCentreVisible = false;
                    ButtonCentreText = String.Empty;
                    ButtonRightVisible = true;
                    ButtonRightText = CANCEL;
                    break;
                case MessageBoxButtonsEnum.YesNo:
                    ButtonLeftVisible = true;
                    ButtonLeftText = YES;
                    ButtonCentreVisible = false;
                    ButtonCentreText = String.Empty;
                    ButtonRightVisible = true;
                    ButtonRightText = NO;
                    break;
                case MessageBoxButtonsEnum.YesNoCancel:
                    ButtonLeftVisible = true;
                    ButtonLeftText = YES;
                    ButtonCentreVisible = true;
                    ButtonCentreText = NO;
                    ButtonRightVisible = true;
                    ButtonRightText = CANCEL;
                    break;
            }
        }

        /// <summary>
        /// Gets the type of message that is converted to an image by the <see cref="MessageTextToImageConverter"/>.
        /// </summary>
        public string Type { get { return message.Type; } }

        /// <summary>
        /// Gets the message to display.
        /// </summary>
        public string Message { get { return message.Text ?? String.Empty; } }

        /// <summary>
        /// Gets the message title.
        /// </summary>
        public string Title { get { return message.Title ?? String.Empty; } }

        /// <summary>
        /// Gets or sets a value indicating whether you can copy the message to a clipboard.
        /// </summary>
        public bool CopyToClipboardEnabled { get; private set; }

        /// <summary>
        /// Gets the type of image to display for the clipboard
        /// once converted to image by <see cref="MessageTextToImageConverter"/>.
        /// </summary>
        public string Clipboard { get { return "Clipboard"; } }

        /// <summary>
        /// Gets the message result.
        /// </summary>
        public MessageBoxResultEnum MessageResult { get; set; }

        /// <summary>
        /// Gets text for the left button.
        /// </summary>
        public string ButtonLeftText { get; set; }

        /// <summary>
        /// Gets the value to indicate whether the left button is visible.
        /// </summary>
        public bool ButtonLeftVisible { get; set; }

        /// <summary>
        /// Gets text for the centre button.
        /// </summary>
        public string ButtonCentreText { get; set; }

        /// <summary>
        /// Gets the value to indicate whether the centre button is visible.
        /// </summary>
        public bool ButtonCentreVisible { get; set; }

        /// <summary>
        /// Gets text for the right button.
        /// </summary>
        public string ButtonRightText { get; set; }

        /// <summary>
        /// Gets the value to indicate whether the right button is visible.
        /// </summary>
        public bool ButtonRightVisible { get; set; }

        /// <summary>
        /// Handles the button click.
        /// </summary>
        /// <param name="button">The type of button clicked.</param>
        public void OnButtonClick(string button)
        {
            if (!isClosing)
            {
                switch (button)
                {
                    case OK:
                        MessageResult = MessageBoxResultEnum.Ok;
                        break;
                    case CANCEL:
                        MessageResult = MessageBoxResultEnum.Cancel;
                        break;
                    case YES:
                        MessageResult = MessageBoxResultEnum.Yes;
                        break;
                    case NO:
                        MessageResult = MessageBoxResultEnum.No;
                        break;
                }
            }

            isClosing = true;
        }

        /// <summary>
        /// Copies the message and stack trace to the clipboard.
        /// </summary>
        public void OnCopyClick()
        {
            string text = String.Format("{0}\r\n{1}", message.Title, message.Text);
            System.Windows.Clipboard.Clear();
            System.Windows.Clipboard.SetText(text);
        }
    }
}