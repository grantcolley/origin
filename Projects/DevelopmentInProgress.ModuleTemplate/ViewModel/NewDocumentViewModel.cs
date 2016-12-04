using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.ViewModel;

namespace DevelopmentInProgress.ModuleTemplate.ViewModel
{
    public class NewDocumentViewModel : DocumentViewModel
    {
        public NewDocumentViewModel(ViewModelContext viewModelContext)
            : base(viewModelContext)
        {
        }

        protected async override void OnPublished(object data)
        {
            // Do stuff here...
        }

        protected async override void SaveDocument()
        {
            // Save stuff here...
        }
    }
}
