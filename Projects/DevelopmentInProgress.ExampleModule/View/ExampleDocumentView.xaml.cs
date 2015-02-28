using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.View;
using DevelopmentInProgress.ExampleModule.ViewModel;

namespace DevelopmentInProgress.ExampleModule.View
{
    /// <summary>
    /// Interaction logic for ExampleDocumentView.xaml
    /// </summary>
    public partial class ExampleDocumentView : DocumentViewBase
    {
        public ExampleDocumentView(IViewContext viewContext, ExampleDocumentViewModel exampleDocumentViewModel)
            : base(viewContext, exampleDocumentViewModel, Module.ModuleName)
        {
            InitializeComponent();

            DataContext = exampleDocumentViewModel;
        }
    }
}
