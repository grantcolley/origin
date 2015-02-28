//-----------------------------------------------------------------------
// <copyright file="ModalViewHost.cs" company="Development In Progress Ltd">
//     Copyright © 2012. All rights reserved.
// </copyright>
// <author>Grant Colley</author>
//-----------------------------------------------------------------------

using DevelopmentInProgress.Origin.Messages;
using DevelopmentInProgress.Origin.Navigation;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DevelopmentInProgress.Origin.View
{
    /// <summary>
    /// Interaction logic for ModalViewHost.xaml
    /// </summary>
    public partial class ModalViewHost : Window
    {
        public ModalViewHost(ModalViewBase modalViewBase)
        {
            InitializeComponent();

            MainContent.Content = modalViewBase;
            DataContext = modalViewBase.DataContext;
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
                mesage.Title = Title;
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
