using DevelopmentInProgress.ModuleTemplate.ViewModel;
using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.View;

namespace DevelopmentInProgress.ModuleTemplate.View
{
    /// <summary>
    /// Interaction logic for NewDocumentView.xaml
    /// </summary>
    public partial class NewDocumentView : DocumentViewBase
    {
        public NewDocumentView(IViewContext viewContext, NewDocumentViewModel newDocumentViewModel)
            : base(viewContext, newDocumentViewModel, Module.ModuleName)
        {
            InitializeComponent();

            DataContext = newDocumentViewModel;
        }
    }
}
