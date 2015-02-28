//-----------------------------------------------------------------------
// <copyright file="DocumentViewHost.cs" company="Development In Progress Ltd">
//     Copyright © 2012. All rights reserved.
// </copyright>
// <author>Grant Colley</author>
//-----------------------------------------------------------------------

using DevelopmentInProgress.Origin.Messages;
using DevelopmentInProgress.Origin.Navigation;
using DevelopmentInProgress.Origin.RegionAdapters;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace DevelopmentInProgress.Origin.View
{
    /// <summary>
    /// The host for the document view set as the content for the
    /// regions host control in <see cref="DockingManagerBehavior"/>. 
    /// The <see cref="DocumentViewHost"/> provides common functionality
    /// such as navigation history, refresh and messaging.
    /// </summary>
    public partial class DocumentViewHost : UserControl
    {
        public DocumentViewHost(DocumentViewBase documentViewBase)
        {
            InitializeComponent();

            MainContent.Content = documentViewBase;
            DataContext = documentViewBase.DataContext;
            ModuleName = documentViewBase.ModuleName;
        }

        /// <summary>
        /// Gets or sets the module name.
        /// </summary>
        public string ModuleName { get; private set; }

        /// <summary>
        /// Gets an instance of the <see cref="DocumentViewBase"/>.
        /// </summary>
        public DocumentViewBase View
        {
            get { return MainContent.Content as DocumentViewBase; }
        }

        /// <summary>
        /// When a user double clicks on a message then open the message in a message box.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        private void MessageMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            var contentControl = sender as ContentControl;
            if (contentControl == null)
            {
                return;
            }

            var mesage = contentControl.DataContext as Message;
            if (mesage == null)
            {
                return;
            }

            if (String.IsNullOrEmpty(mesage.Title))
            {
                mesage.Title = ModuleName;
            }

            var modalManager = ServiceLocator.Current.GetInstance<ModalNavigator>();
            var messageBoxSettings = new MessageBoxSettings
            {
                MessageBoxButtons = MessageBoxButtonsEnum.Ok,
                CopyToClipboardEnabled = true,
                Message = mesage
            };

            modalManager.ShowMessageBox(messageBoxSettings);
        }
    }
}
