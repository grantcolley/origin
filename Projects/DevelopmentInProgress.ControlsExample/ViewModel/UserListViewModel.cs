using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.ViewModel;

namespace DevelopmentInProgress.ControlsExample.ViewModel
{
    public class UserListViewModel : DocumentViewModel
    {
        public UserListViewModel(ViewModelContext viewModelContext)
            : base(viewModelContext)
        {
        }

        protected async override void OnPublished(object data)
        {
        }

        protected async override void SaveDocument()
        {
        }
    }
}
