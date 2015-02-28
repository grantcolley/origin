using System;
using DevelopmentInProgress.ModuleTemplate.View;
using DevelopmentInProgress.ModuleTemplate.ViewModel;
using DevelopmentInProgress.Origin.Module;
using DevelopmentInProgress.Origin.Navigation;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

namespace DevelopmentInProgress.ModuleTemplate
{
    public class Module : ModuleBase
    {
        public const string ModuleName = "Module Template";

        public Module(IUnityContainer container, ModuleNavigator moduleNavigator, ILoggerFacade logger)
            : base(container, moduleNavigator, logger)
        {
        }

        public override void Initialize()
        {
            Container.RegisterType<Object, NewDocumentView>(typeof(NewDocumentView).Name);
            Container.RegisterType<NewDocumentViewModel>(typeof(NewDocumentViewModel).Name);

            var moduleSettings = new ModuleSettings();
            moduleSettings.ModuleName = ModuleName;
            moduleSettings.ModuleImagePath = @"/DevelopmentInProgress.ModuleTemplate;component/Images/ModuleTemplate.png";

            var moduleGroup = new ModuleGroup();
            moduleGroup.ModuleGroupName = "Module Template";

            var newDocument = new ModuleGroupItem();
            newDocument.ModuleGroupItemName = "New Document";
            newDocument.TargetView = typeof(NewDocumentView).Name;
            newDocument.TargetViewTitle = "New Document";
            newDocument.ModuleGroupItemImagePath = @"/DevelopmentInProgress.ModuleTemplate;component/Images/NewDocument.png";

            moduleGroup.ModuleGroupItems.Add(newDocument);
            moduleSettings.ModuleGroups.Add(moduleGroup);
            ModuleNavigator.AddModuleNavigation(moduleSettings);

            Logger.Log("Initialize DevelopmentInProgress.ModuleTemplate Complete", Category.Info, Priority.None);
        }
    }
}
