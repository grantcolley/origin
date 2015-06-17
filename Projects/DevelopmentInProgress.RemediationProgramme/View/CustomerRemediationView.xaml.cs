using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using DevelopmentInProgress.RemediationProgramme.ViewModel;
using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.View;

namespace DevelopmentInProgress.RemediationProgramme.View
{
    /// <summary>
    /// Interaction logic for RemediationWorkflowView.xaml
    /// </summary>
    public partial class CustomerRemediationView : DocumentViewBase
    {
        public CustomerRemediationView(IViewContext viewContext, CustomerRemediationViewModel customerRemediationViewModel)
            : base(viewContext, customerRemediationViewModel, Module.ModuleName)
        {
            InitializeComponent();

            DataContext = customerRemediationViewModel;
        }
    }
}
