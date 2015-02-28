//-----------------------------------------------------------------------
// <copyright file="Message.cs" company="Development In Progress Ltd">
//     Copyright © 2012. All rights reserved.
// </copyright>
// <author>Grant Colley</author>
//-----------------------------------------------------------------------

using DevelopmentInProgress.Origin.Converters;

namespace DevelopmentInProgress.Origin.Messages
{
    /// <summary>
    /// Details of the message to be displayed.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Gets or sets the type of message to display.
        /// </summary>
        public MessageTypeEnum MessageType { get; set; }

        /// <summary>
        /// Gets or sets the message text to display.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the title of the message to display.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets a text representation of the message to be 
        /// converted to image by <see cref="MessageTextToImageConverter"/>.
        /// </summary>
        public string Type
        {
            get
            {
                return MessageType == MessageTypeEnum.Error ? "Error"
                    : MessageType == MessageTypeEnum.Warn ? "Warn" 
                    : MessageType == MessageTypeEnum.Question ? "Question" : "Info";
            }
        }
    }
}
