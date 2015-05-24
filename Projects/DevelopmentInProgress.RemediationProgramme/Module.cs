using System;
using DevelopmentInProgress.Origin.Module;
using DevelopmentInProgress.Origin.Navigation;
using DevelopmentInProgress.RemediationProgramme.View;
using DevelopmentInProgress.RemediationProgramme.ViewModel;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

namespace DevelopmentInProgress.RemediationProgramme
{
    public class Module : ModuleBase
    {
        public const string ModuleName = "Remediation Programme";

        public Module(IUnityContainer container, ModuleNavigator moduleNavigator, ILoggerFacade logger)
            : base(container, moduleNavigator, logger)
        {
        }

        public override void Initialize()
        {
            Container.RegisterType<Object, CustomerRemediationView>(typeof(CustomerRemediationView).Name);
            Container.RegisterType<CustomerRemediationViewModel>(typeof(CustomerRemediationViewModel).Name);

            var moduleSettings = new ModuleSettings();
            moduleSettings.ModuleName = ModuleName;
            moduleSettings.ModuleImagePath = @"/DevelopmentInProgress.RemediationProgramme;component/Images/RemediationProgramme.png";

            var remediationWorkflowGroup = new ModuleGroup();
            remediationWorkflowGroup.ModuleGroupName = "Customer Remediation";

            var remediationWorkflow = new ModuleGroupItem();
            remediationWorkflow.ModuleGroupItemName = "Customer Remediation";
            remediationWorkflow.TargetView = typeof(CustomerRemediationView).Name;
            remediationWorkflow.TargetViewTitle = "Customer Remediation";
            remediationWorkflow.ModuleGroupItemImagePath = @"/DevelopmentInProgress.RemediationProgramme;component/Images/CustomerRemediation.png";

            remediationWorkflowGroup.ModuleGroupItems.Add(remediationWorkflow);

            moduleSettings.ModuleGroups.Add(remediationWorkflowGroup);

            ModuleNavigator.AddModuleNavigation(moduleSettings);

            Logger.Log("Initialize DevelopmentInProgress.RemediationProgramme Complete", Category.Info, Priority.None);
        }
    }
}
