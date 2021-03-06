﻿using System;
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
            Container.RegisterType<Object, ExampleDocumentView>(typeof(ExampleDocumentView).Name);
            Container.RegisterType<ExampleDocumentViewModel>(typeof(ExampleDocumentViewModel).Name);
            Container.RegisterType<Object, ExampleDocumentMessagesView>(typeof(ExampleDocumentMessagesView).Name);
            Container.RegisterType<ExampleDocumentMessagesViewModel>(typeof(ExampleDocumentMessagesViewModel).Name);
            Container.RegisterType<Object, ExampleDocumentNavigationView>(typeof(ExampleDocumentNavigationView).Name);
            Container.RegisterType<ExampleDocumentNavigationViewModel>(typeof(ExampleDocumentNavigationViewModel).Name);
            Container.RegisterType<Object, CustomerRemediationView>(typeof(CustomerRemediationView).Name);
            Container.RegisterType<CustomerRemediationViewModel>(typeof(CustomerRemediationViewModel).Name);

            var moduleSettings = new ModuleSettings();
            moduleSettings.ModuleName = ModuleName;
            moduleSettings.ModuleImagePath = @"/DevelopmentInProgress.ExampleModule;component/Images/ExampleModule.png";

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

            var remediationWorkflowGroup = new ModuleGroup();
            remediationWorkflowGroup.ModuleGroupName = "Customer Remediation";

            var remediationWorkflow = new ModuleGroupItem();
            remediationWorkflow.ModuleGroupItemName = "Customer Remediation";
            remediationWorkflow.TargetView = typeof(CustomerRemediationView).Name;
            remediationWorkflow.TargetViewTitle = "Customer Remediation";
            remediationWorkflow.ModuleGroupItemImagePath = @"/DevelopmentInProgress.ExampleModule;component/Images/CustomerRemediation.png";

            remediationWorkflowGroup.ModuleGroupItems.Add(remediationWorkflow);

            moduleSettings.ModuleGroups.Add(moduleGroup);
            moduleSettings.ModuleGroups.Add(remediationWorkflowGroup);

            ModuleNavigator.AddModuleNavigation(moduleSettings);

            Logger.Log("Initialize DevelopmentInProgress.ExampleModule Complete", Category.Info, Priority.None);
        }
    }
}
