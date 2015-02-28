//-----------------------------------------------------------------------
// <copyright file="ModalViewModel.cs" company="Development In Progress Ltd">
//     Copyright © 2012. All rights reserved.
// </copyright>
// <author>Grant Colley</author>
//-----------------------------------------------------------------------

using System;
using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.Navigation;
using System.Collections.Generic;

namespace DevelopmentInProgress.Origin.ViewModel
{
    /// <summary>
    /// Base view model for modal windows. Inherits <see cref="ViewModelBase"/>.
    /// </summary>
    public abstract class ModalViewModel : ViewModelBase
    {
        private Dictionary<string, object> parameters;

        /// <summary>
        /// Initialises a new instance of the <see cref="ModalViewModel"/> class.
        /// </summary>
        /// <param name="viewModelContext">The <see cref="ViewModelContext"/>.</param>
        protected ModalViewModel(IViewModelContext viewModelContext)
            : base(viewModelContext)
        {
        }

        /// <summary>
        /// Output of the modal window which can be accessed from 
        /// the calling code when the modal window is closed.
        /// </summary>
        public object Output { get; set; }

        /// <summary>
        /// Called by the <see cref="ModalNavigator"/> to pass a collection of parameters.
        /// </summary>
        /// <param name="param">A dictionary of parameters.</param>
        public void Publish(Dictionary<string, object> param)
        {
            parameters = param;
            DataPublished();
            OnPropertyChanged(String.Empty);
        }

        /// <summary>
        /// To be overriden by the the sub class.
        /// </summary>
        /// <param name="param">A dictionary of parameters.</param>
        /// <returns>The results of processing the method asynchronously</returns>
        protected virtual ProcessAsyncResult OnPublishedAsync(Dictionary<string, object> param)
        {
            return new ProcessAsyncResult();
        }

        /// <summary>
        /// Implemnents the abstract OnPublished method of the <see cref="ViewModelBase"/>.
        /// Calls OnPublished to be implemented by the subclass.
        /// </summary>
        /// <returns>The results of processing the method asynchronously</returns>
        protected override ProcessAsyncResult OnPublishedAsync()
        {
            return OnPublishedAsync(parameters);
        }
    }
}
