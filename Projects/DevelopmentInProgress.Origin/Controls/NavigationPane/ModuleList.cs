//-----------------------------------------------------------------------
// <copyright file="ModuleList.cs" company="Development In Progress Ltd">
//     Copyright © 2012. All rights reserved.
// </copyright>
// <author>Grant Colley</author>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevelopmentInProgress.Origin.Command;

namespace DevelopmentInProgress.Origin.Controls.NavigationPane
{
    /// <summary>
    /// The module list class.
    /// </summary>
    public class ModuleList : Control
    {
        private ICommand selectionChangedCommand;
        private ICommand expanderChangedCommand;

        private readonly static DependencyProperty SelectedModuleProperty;
        private readonly static DependencyProperty ModulesProperty;
        private readonly static DependencyProperty IsExpandedProperty;
        private readonly static RoutedEvent ItemSelectedEvent;

        /// <summary>
        /// Static constructor for the module list class for registering dependency properties and events.
        /// </summary>
        static ModuleList()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ModuleList),
                new FrameworkPropertyMetadata(typeof(ModuleList)));

            SelectedModuleProperty = DependencyProperty.Register("SelectedModule", typeof(ModuleListItem), typeof(ModuleList));

            ModulesProperty = DependencyProperty.Register("Modules", typeof(List<ModuleListItem>),
                typeof(ModuleList), new FrameworkPropertyMetadata(new List<ModuleListItem>()));

            IsExpandedProperty = DependencyProperty.Register("IsExpanded", typeof(bool), typeof(ModuleList));

            ItemSelectedEvent = EventManager.RegisterRoutedEvent(
                "ItemSelected", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ModuleList));
        }

        /// <summary>
        /// Initializes a new instance of the ModuleList class.
        /// </summary>
        public ModuleList()
        {
            Modules = new List<ModuleListItem>();
            selectionChangedCommand = new DelegateCommand(OnSelectionChanged);
            expanderChangedCommand = new DelegateCommand(OnExpanderChanged);
            IsExpanded = true;
        }
        
        /// <summary>
        /// Uses System.Windows.Interactivity in the Xaml where the 
        /// ListBox.SelectionChanged event triggers the SelectionChangedCommand. 
        /// </summary>
        public ICommand SelectionChangedCommand
        {
            get { return selectionChangedCommand; }
            set { selectionChangedCommand = value; }
        }

        /// <summary>
        /// Uses System.Windows.Interactivity in the Xaml where the
        /// Image.MouseDown event triggers the ExpanderChangedCommand.
        /// </summary>
        public ICommand ExpanderChangedCommand
        {
            get { return expanderChangedCommand; }
            set { expanderChangedCommand = value; }
        }

        /// <summary>
        /// Gets or sets the selected module list item.
        /// </summary>
        public ModuleListItem SelectedModule
        {
            get { return (ModuleListItem)GetValue(SelectedModuleProperty); }
            set { SetValue(SelectedModuleProperty, value); }
        }

        /// <summary>
        /// Gets or sets a list of module items.
        /// </summary>
        public List<ModuleListItem> Modules
        {
            get { return (List<ModuleListItem>)GetValue(ModulesProperty); }
            set { SetValue(ModulesProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the module list is expanded or collapsed.
        /// </summary>
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the module list item is selected.
        /// </summary>
        public event RoutedEventHandler ItemSelected
        {
            add { AddHandler(ItemSelectedEvent, value); }
            remove { RemoveHandler(ItemSelectedEvent, value); }
        }

        /// <summary>
        /// Raises the ItemSelectedEvent passing in the selected ModuleListItem.
        /// OnSelectionChanged handles the ListBox.SelectionChanged event which  
        /// triggers the SelectionChangedCommand using System.Windows.Interactivity.
        /// </summary>
        /// <param name="arg">The selected ModuleListItem.</param>
        private void OnSelectionChanged(object arg)
        {
            if (arg == null)
            {
                return;
            }

            var moduleListItem = arg as ModuleListItem;
            var args = new RoutedEventArgs(ItemSelectedEvent, moduleListItem);
            RaiseEvent(args);
        }

        /// <summary>
        /// Toggles the module list expanded / unexpanded.
        /// </summary>
        /// <param name="arg">Null</param>
        private void OnExpanderChanged(object arg)
        {

            if (arg == null)
            {
                return;
            }

            SelectedModule = arg as ModuleListItem;
            IsExpanded = !IsExpanded;
        }
    }
}
