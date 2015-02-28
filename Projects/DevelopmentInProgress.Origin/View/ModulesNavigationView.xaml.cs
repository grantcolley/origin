//-----------------------------------------------------------------------
// <copyright file="ModulesNavigationView.cs" company="Development In Progress Ltd">
//     Copyright © 2012. All rights reserved.
// </copyright>
// <author>Grant Colley</author>
//-----------------------------------------------------------------------

using DevelopmentInProgress.Origin.Controls.NavigationPane;
using DevelopmentInProgress.Origin.Navigation;
using DevelopmentInProgress.Origin.RegionAdapters;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DevelopmentInProgress.Origin.View
{
    /// <summary>
    /// Interaction logic for ModulesNavigationView.xaml
    /// </summary>
    public partial class ModulesNavigationView : UserControl
    {
        private readonly NavigationManager navigationManager;
        private readonly Dictionary<string, NavigationSettings> navigationSettingsList;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModulesNavigationView"/> class. 
        /// </summary>
        /// <param name="navigationManager">The navigation manager.</param>
        public ModulesNavigationView(NavigationManager navigationManager)
        {
            navigationSettingsList = new Dictionary<string, NavigationSettings>();
            this.navigationManager = navigationManager;

            InitializeComponent();

            moduleList.ItemSelected += SelectedModuleListItem;
        }

        /// <summary>
        /// Raises an event notifying the <see cref="DockingManagerBehavior"/> 
        /// which module has been selected. 
        /// </summary>
        public static event EventHandler<ModuleEventArgs> ModuleSelected;

        /// <summary>
        /// Adds a new module to the navigation view. Called by the <see cref="ModuleNavigator"/>.
        /// </summary>
        /// <param name="moduleSettings">Module settings.</param>
        public void AddModule(ModuleSettings moduleSettings)
        {
            var moduleListItem = new ModuleListItem();
            moduleListItem.ModuleName = moduleSettings.ModuleName;
            moduleListItem.ImageLocation = moduleSettings.ModuleImagePath;

            foreach (ModuleGroup moduleGroup in moduleSettings.ModuleGroups)
            {
                var groupList = new GroupList {GroupListName = moduleGroup.ModuleGroupName};

                foreach (ModuleGroupItem moduleGroupItem in moduleGroup.ModuleGroupItems)
                {
                    var groupListItem = new GroupListItem
                    {
                        ItemName = moduleGroupItem.ModuleGroupItemName,
                        ImageLocation = moduleGroupItem.ModuleGroupItemImagePath
                    };

                    groupListItem.ItemClicked += GroupListItemItemClicked;
                    groupList.GroupListItems.Add(groupListItem);

                    var navigationSettings = new NavigationSettings
                    {
                        Title = moduleGroupItem.TargetViewTitle,
                        View = moduleGroupItem.TargetView
                    };

                    string navigationKey = String.Format("{0}.{1}.{2}",
                        moduleListItem.ModuleName,
                        groupList.GroupListName,
                        groupListItem.ItemName);

                    groupListItem.Tag = navigationKey;
                    navigationSettingsList.Add(navigationKey, navigationSettings);
                }

                moduleListItem.Groups.Add(groupList);
            }

            moduleList.Modules.Add(moduleListItem);
        }

        /// <summary>
        /// Opens a new document.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        private void GroupListItemItemClicked(object sender, RoutedEventArgs e)
        {
            var groupListItem = (GroupListItem)e.Source;
            string navigationKey = groupListItem.Tag.ToString();
            NavigationSettings navigationSettings;
            if (navigationSettingsList.TryGetValue(navigationKey, out navigationSettings))
            {
                navigationManager.NavigateDocumentRegion(navigationSettings);
            }
        }

        /// <summary>
        /// Raises the <see cref="ModuleSelected"/> event which is handled by the <see cref="DockingManagerBehavior"/>. 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">Event arguments.</param>
        private void SelectedModuleListItem(object sender, RoutedEventArgs e)
        {
            var moduleListItem = (ModuleListItem)e.OriginalSource;
            var moduleSelected = ModuleSelected;
            if (moduleSelected != null)
            {
                var modulePaneEventArgs = new ModuleEventArgs(moduleListItem.ModuleName);
                moduleSelected(this, modulePaneEventArgs);
            }

            e.Handled = true;
        }
    }
}
