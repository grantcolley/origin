using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.View;
using DevelopmentInProgress.ExampleModule.ViewModel;

namespace DevelopmentInProgress.ExampleModule.View
{
    /// <summary>
    /// Interaction logic for ExampleDocumentNavigationView.xaml
    /// </summary>
    public partial class ExampleDocumentNavigationView : DocumentViewBase
    {
        public ExampleDocumentNavigationView(IViewContext viewContext, ExampleDocumentNavigationViewModel exampleDocumentNavigationViewModel)
            : base(viewContext, exampleDocumentNavigationViewModel, Module.ModuleName)
        {
            InitializeComponent();

            DataContext = exampleDocumentNavigationViewModel;
        }
    }
}
