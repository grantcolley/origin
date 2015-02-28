//-----------------------------------------------------------------------
// <copyright file="ProcessAsyncResult.cs" company="Development In Progress Ltd">
//     Copyright © 2012. All rights reserved.
// </copyright>
// <author>Grant Colley</author>
//-----------------------------------------------------------------------

using System;

namespace DevelopmentInProgress.Origin.ViewModel
{
    /// <summary>
    /// The results of processing a task asynchronously when calling the <see cref="ViewModelBase"/> ProcessAsync method.
    /// </summary>
    public class ProcessAsyncResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether the asynchronous task faulted.
        /// </summary>
        public bool IsFaulted { get; set; }

        /// <summary>
        /// Gets or sets the aggregated exception thrown by the asynchrounous task.
        /// </summary>
        public AggregateException FlattenedAggregateException { get; set; }

        /// <summary>
        /// Gets or sets a data object that can be used during the asynchronous task.
        /// E.g. collect data in the background task that is passed to the completion task.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets the state to be passed into to the asynchronous task.
        /// </summary>
        public object State { get; set; }
    }
}
