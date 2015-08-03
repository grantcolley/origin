using DevelopmentInProgress.ExampleModule.ViewModel;
using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.View;

namespace DevelopmentInProgress.ExampleModule.View
{
    /// <summary>
    /// Interaction logic for CustomerRemediationView.xaml
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
