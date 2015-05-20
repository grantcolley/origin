using System.Windows.Input;
using DevelopmentInProgress.ExampleModule.ViewModel;
using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.View;

namespace DevelopmentInProgress.ExampleModule.View
{
    /// <summary>
    /// Interaction logic for RemediationWorkflowView.xaml
    /// </summary>
    public partial class RemediationWorkflowView : DocumentViewBase
    {
        public RemediationWorkflowView(IViewContext viewContext, RemediationWorkflowViewModel remediationWorkflowViewModel)
            : base(viewContext, remediationWorkflowViewModel, Module.ModuleName)
        {
            InitializeComponent();

            DataContext = remediationWorkflowViewModel;
        }

        private void OnPreviewTextIsDecimal(object sender, TextCompositionEventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
