//-----------------------------------------------------------------------
// <copyright file="GroupList.cs" company="Development In Progress Ltd">
//     Copyright © 2012. All rights reserved.
// </copyright>
// <author>Grant Colley</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DevelopmentInProgress.Origin.Controls.NavigationPane
{
    /// <summary>
    /// The group list class.
    /// </summary>
    public class GroupList : Control
    {
        private readonly static DependencyProperty GroupListNameProperty;
        private readonly static DependencyProperty GroupListItemsProperty;

        /// <summary>
        /// Static constructor for the group list class for registering dependency properties and events.
        /// </summary>
        static GroupList()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupList), new FrameworkPropertyMetadata(typeof(GroupList)));

            GroupListNameProperty = DependencyProperty.Register("GroupListName", typeof(string), typeof(GroupList));
            GroupListItemsProperty = DependencyProperty.Register("GroupListItems", typeof(List<GroupListItem>), 
                typeof(GroupList), new FrameworkPropertyMetadata(new List<GroupListItem>()));
        }

        /// <summary>
        /// Initializes a new instance of the GroupList class.
        /// </summary>
        public GroupList()
        {
            GroupListItems = new List<GroupListItem>();
        }

        /// <summary>
        /// Gets or sets the group list name.
        /// </summary>
        public string GroupListName
        {
            get { return GetValue(GroupListNameProperty).ToString(); }
            set { SetValue(GroupListNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets the group list items.
        /// </summary>
        public List<GroupListItem> GroupListItems
        {
            get { return (List<GroupListItem>)GetValue(GroupListItemsProperty); }
            set { SetValue(GroupListItemsProperty, value); }
        }
    }
}