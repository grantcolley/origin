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

        protected override ProcessAsyncResult OnPublishedAsync(object data)
        {
            return base.OnPublishedAsync(data);
        }

        protected override void OnPublishedCompleted(ProcessAsyncResult processAsyncResult)
        {
            base.OnPublishedCompleted(processAsyncResult);
        }

        protected override ProcessAsyncResult SaveDocumentAsync()
        {
            return base.SaveDocumentAsync();
        }

        protected override void SaveDocumentCompleted(ProcessAsyncResult processAsyncResult)
        {
            base.SaveDocumentCompleted(processAsyncResult);
        }
    }
}
