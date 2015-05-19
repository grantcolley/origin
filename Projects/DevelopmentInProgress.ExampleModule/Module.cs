using System;
using DevelopmentInProgress.Origin.Module;
using DevelopmentInProgress.Origin.Navigation;
using DevelopmentInProgress.ExampleModule.View;
using DevelopmentInProgress.ExampleModule.ViewModel;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

namespace DevelopmentInProgress.ExampleModule
{
    public class Module : ModuleBase
    {
        public const string ModuleName = "Example Module";

        public Module(IUnityContainer container, ModuleNavigator moduleNavigator, ILoggerFacade logger)
            : base(container, moduleNavigator, logger)
        {
        }

        public override void Initialize()
        {
            Container.RegisterType<Object, RemediationWorkflowView>(typeof(RemediationWorkflowView).Name);
            Container.RegisterType<RemediationWorkflowViewModel>(typeof(RemediationWorkflowViewModel).Name);
            Container.RegisterType<Object, ExampleDocumentView>(typeof(ExampleDocumentView).Name);
            Container.RegisterType<ExampleDocumentViewModel>(typeof(ExampleDocumentViewModel).Name);
            Container.RegisterType<Object, ExampleDocumentMessagesView>(typeof(ExampleDocumentMessagesView).Name);
            Container.RegisterType<ExampleDocumentMessagesViewModel>(typeof(ExampleDocumentMessagesViewModel).Name);
            Container.RegisterType<Object, ExampleDocumentNavigationView>(typeof(ExampleDocumentNavigationView).Name);
            Container.RegisterType<ExampleDocumentNavigationViewModel>(typeof(ExampleDocumentNavigationViewModel).Name);

            var moduleSettings = new ModuleSettings();
            moduleSettings.ModuleName = ModuleName;
            moduleSettings.ModuleImagePath = @"/DevelopmentInProgress.ExampleModule;component/Images/ExampleModule.png";

            #region Example Module

            var moduleGroup = new ModuleGroup();
            moduleGroup.ModuleGroupName = "Example Module";

            var exampleDocument = new ModuleGroupItem();
            exampleDocument.ModuleGroupItemName = "Document Example";
            exampleDocument.TargetView = typeof(ExampleDocumentView).Name;
            exampleDocument.TargetViewTitle = "Document Example";
            exampleDocument.ModuleGroupItemImagePath = @"/DevelopmentInProgress.ExampleModule;component/Images/DocumentEdit.png";

            var documentMessages = new ModuleGroupItem();
            documentMessages.ModuleGroupItemName = "Document Messages";
            documentMessages.TargetView = typeof(ExampleDocumentMessagesView).Name;
            documentMessages.TargetViewTitle = "Document Messages";
            documentMessages.ModuleGroupItemImagePath = @"/DevelopmentInProgress.ExampleModule;component/Images/DocumentMessages.png";

            var documentNavigation = new ModuleGroupItem();
            documentNavigation.ModuleGroupItemName = "Document Navigation";
            documentNavigation.TargetView = typeof(ExampleDocumentNavigationView).Name;
            documentNavigation.TargetViewTitle = "Document Navigation";
            documentNavigation.ModuleGroupItemImagePath = @"/DevelopmentInProgress.ExampleModule;component/Images/DocumentNavigation.png";

            moduleGroup.ModuleGroupItems.Add(exampleDocument);
            moduleGroup.ModuleGroupItems.Add(documentMessages);
            moduleGroup.ModuleGroupItems.Add(documentNavigation);

            #endregion

            #region Remediation Workflow

            var remediationWorkflowGroup = new ModuleGroup();
            remediationWorkflowGroup.ModuleGroupName = "Remediation Workflow";

            var remediationWorkflow = new ModuleGroupItem();
            remediationWorkflow.ModuleGroupItemName = "Remediation Workflow";
            remediationWorkflow.TargetView = typeof(RemediationWorkflowView).Name;
            remediationWorkflow.TargetViewTitle = "Remediation Workflow";
            remediationWorkflow.ModuleGroupItemImagePath = @"/DevelopmentInProgress.ExampleModule;component/Images/Remediation.png";

            remediationWorkflowGroup.ModuleGroupItems.Add(remediationWorkflow);

            #endregion

            moduleSettings.ModuleGroups.Add(remediationWorkflowGroup);
            moduleSettings.ModuleGroups.Add(moduleGroup);

            ModuleNavigator.AddModuleNavigation(moduleSettings);

            Logger.Log("Initialize DevelopmentInProgress.ExampleModule Complete", Category.Info, Priority.None);
        }
    }
}
