using System;
using DevelopmentInProgress.ControlsExample.View;
using DevelopmentInProgress.ControlsExample.ViewModel;
using DevelopmentInProgress.Origin.Module;
using DevelopmentInProgress.Origin.Navigation;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

namespace DevelopmentInProgress.ControlsExample
{
    public class Module : ModuleBase
    {
        public const string ModuleName = "Manage Users";

        public Module(IUnityContainer container, ModuleNavigator moduleNavigator, ILoggerFacade logger)
            : base(container, moduleNavigator, logger)
        {
        }

        public override void Initialize()
        {
            Container.RegisterType<Object, UserListView>(typeof(UserListView).Name);
            Container.RegisterType<UserListViewModel>(typeof(UserListViewModel).Name);

            var moduleSettings = new ModuleSettings();
            moduleSettings.ModuleName = ModuleName;
            moduleSettings.ModuleImagePath = @"/DevelopmentInProgress.ControlsExample;component/Images/User_Manage.png";

            var usersGroup = new ModuleGroup();
            usersGroup.ModuleGroupName = "Users";

            var userList = new ModuleGroupItem();
            userList.ModuleGroupItemName = "User List";
            userList.TargetView = typeof(UserListView).Name;
            userList.TargetViewTitle = "User List";
            userList.ModuleGroupItemImagePath = @"/DevelopmentInProgress.ControlsExample;component/Images/User_List.png";

            var userAdd = new ModuleGroupItem();
            userAdd.ModuleGroupItemName = "User Add";
            userAdd.TargetView = typeof(UserListView).Name;
            userAdd.TargetViewTitle = "User Add";
            userAdd.ModuleGroupItemImagePath = @"/DevelopmentInProgress.ControlsExample;component/Images/User_Add.png";

            var rolesAndActivitiesGroup = new ModuleGroup();
            rolesAndActivitiesGroup.ModuleGroupName = "Roles & Activities";

            var roles = new ModuleGroupItem();
            roles.ModuleGroupItemName = "Roles";
            roles.TargetView = typeof(UserListView).Name;
            roles.TargetViewTitle = "Roles";
            roles.ModuleGroupItemImagePath = @"/DevelopmentInProgress.ControlsExample;component/Images/Roles.png";

            var activities = new ModuleGroupItem();
            activities.ModuleGroupItemName = "Activities";
            activities.TargetView = typeof(UserListView).Name;
            activities.TargetViewTitle = "Activities";
            activities.ModuleGroupItemImagePath = @"/DevelopmentInProgress.ControlsExample;component/Images/Activities.png";

            usersGroup.ModuleGroupItems.Add(userList);
            usersGroup.ModuleGroupItems.Add(userAdd);
            moduleSettings.ModuleGroups.Add(usersGroup);

            rolesAndActivitiesGroup.ModuleGroupItems.Add(roles);
            rolesAndActivitiesGroup.ModuleGroupItems.Add(activities);
            moduleSettings.ModuleGroups.Add(rolesAndActivitiesGroup);

            ModuleNavigator.AddModuleNavigation(moduleSettings);

            Logger.Log("Initialize DevelopmentInProgress.ControlsExample Complete", Category.Info, Priority.None);
        }
    }
}
