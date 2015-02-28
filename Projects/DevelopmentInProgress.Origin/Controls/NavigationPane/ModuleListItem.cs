//-----------------------------------------------------------------------
// <copyright file="ModuleListItem.cs" company="Development In Progress Ltd">
//     Copyright © 2012. All rights reserved.
// </copyright>
// <author>Grant Colley</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DevelopmentInProgress.Origin.Controls.NavigationPane
{
    /// <summary>
    /// A navigation module list item. 
    /// </summary>
    public class ModuleListItem : Control, ICommandSource
    {
        private readonly static DependencyProperty ModuleNameProperty;
        private readonly static DependencyProperty ImageLocationProperty;
        private readonly static DependencyProperty GroupsProperty;
        private readonly static DependencyProperty IsSelectedProperty;
        private readonly static DependencyProperty CommandProperty;
        private readonly static DependencyProperty CommandParameterProperty;
        private readonly static DependencyProperty CommandTargetProperty;
        private readonly static RoutedEvent ItemClickedEvent;

        /// <summary>
        /// Static constructor for the module list item class 
        /// for registering dependency properties and events.
        /// </summary>
        static ModuleListItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ModuleListItem), new FrameworkPropertyMetadata(typeof(ModuleListItem)));

            ModuleNameProperty = DependencyProperty.Register(
                "ModuleName", typeof(string), typeof(ModuleListItem));

            ImageLocationProperty = DependencyProperty.Register(
                "ImageLocation", typeof(string), typeof(ModuleListItem));

            GroupsProperty = DependencyProperty.Register(
                "Groups", typeof(List<GroupList>), typeof(GroupList), new FrameworkPropertyMetadata(new List<GroupList>()));

            IsSelectedProperty = DependencyProperty.Register(
                "IsSelected", typeof(bool), typeof(ModuleListItem));

            CommandProperty = DependencyProperty.Register(
                "Command", typeof(ICommand), typeof(ModuleListItem));

            CommandParameterProperty = DependencyProperty.Register(
                "CommandParameter", typeof(object), typeof(ModuleListItem));

            CommandTargetProperty = DependencyProperty.Register(
                "CommandTarget", typeof(UIElement), typeof(ModuleListItem));

            ItemClickedEvent = EventManager.RegisterRoutedEvent(
                "ItemClicked", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ModuleListItem));
        }

        /// <summary>
        /// Initializes a new instance of the ModuleListItem class. 
        /// </summary>
        public ModuleListItem()
        {
            Groups = new List<GroupList>();
        }

        /// <summary>
        /// Gets or sets the module name.
        /// </summary>
        public string ModuleName
        {
            get { return GetValue(ModuleNameProperty).ToString(); }
            set { SetValue(ModuleNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets the image location.
        /// </summary>
        public string ImageLocation
        {
            get { return (string)GetValue(ImageLocationProperty); }
            set { SetValue(ImageLocationProperty, value); }
        }

        /// <summary>
        /// Gets or sets the list of groups.
        /// </summary>
        public List<GroupList> Groups
        {
            get { return (List<GroupList>)GetValue(GroupsProperty); }
            set { SetValue(GroupsProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the module list item is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command parameter.
        /// </summary>
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command target.
        /// </summary>
        public IInputElement CommandTarget
        {
            get { return (UIElement)GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }

        /// <summary>
        /// Gets or sets the item clicked event.
        /// </summary>
        public event RoutedEventHandler ItemClicked
        {
            add { AddHandler(ItemClickedEvent, value); }
            remove { RemoveHandler(ItemClickedEvent, value); }
        }

        /// <summary>
        /// Overrides the mouse's left button up preview.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);
        }

        /// <summary>
        /// Raises the mouse's left button up preview event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            OnItemClicked();
            e.Handled = true;
        }

        /// <summary>
        /// Raises the item clicked event.
        /// </summary>
        private void OnItemClicked()
        {
            var args = new RoutedEventArgs(ItemClickedEvent, this);
            RaiseEvent(args);
            if (Command != null)
            {
                Command.Execute(CommandParameter);
            }
        }
    }
}
