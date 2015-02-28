using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.View;
using DevelopmentInProgress.ExampleModule.ViewModel;

namespace DevelopmentInProgress.ExampleModule.View
{
    /// <summary>
    /// Interaction logic for ExampleDocumentMessagesView.xaml
    /// </summary>
    public partial class ExampleDocumentMessagesView : DocumentViewBase
    {
        public ExampleDocumentMessagesView(IViewContext viewContext, ExampleDocumentMessagesViewModel exampleDocumentMessagesViewModel)
            : base(viewContext, exampleDocumentMessagesViewModel, Module.ModuleName)
        {
            InitializeComponent();

            DataContext = exampleDocumentMessagesViewModel;
        }
    }
}
