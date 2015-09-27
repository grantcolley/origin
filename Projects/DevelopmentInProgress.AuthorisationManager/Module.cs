using System;
using DevelopmentInProgress.AuthorisationManager.View;
using DevelopmentInProgress.AuthorisationManager.ViewModel;
using DevelopmentInProgress.Origin.Module;
using DevelopmentInProgress.Origin.Navigation;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

namespace DevelopmentInProgress.AuthorisationManager
{
    public class Module : ModuleBase
    {
        public Module(IUnityContainer container, ModuleNavigator moduleNavigator, ILoggerFacade logger)
            : base (container, moduleNavigator, logger)
        {            
        }

        public const string ModuleName = "Authorisation Manager";

        public override void Initialize()
        {
            Container.RegisterType<object, ActivityView>(typeof (ActivityView).Name);
            Container.RegisterType<ActivityViewModel>(typeof (ActivityViewModel).Name);

            var authorisationManager = new ModuleSettings()
            {
                ModuleName = ModuleName,
                ModuleImagePath = @"/DevelopmentInProgress.AuthorisationManager;component/Images/AuthorisationManager.png"
            };

            var authorisationGroup = new ModuleGroup()
            {
                ModuleGroupName = "Authorisation"
            };

            var activities = new ModuleGroupItem()
            {
                ModuleGroupItemName = "Activities",
                TargetView = typeof (ActivityView).Name,
                TargetViewTitle = "Activities",
                ModuleGroupItemImagePath =
                    @"\DevelopmentInProgress.AuthorisationManager;component/Images/Activities.png"
            };

            authorisationGroup.ModuleGroupItems.Add(activities);
            authorisationManager.ModuleGroups.Add(authorisationGroup);
            ModuleNavigator.AddModuleNavigation(authorisationManager);

            Logger.Log(String.Format("Initialize {0} Complete", ModuleName), Category.Info, Priority.None);
        }
    }
}
