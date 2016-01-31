using System.Collections.ObjectModel;
using System.Windows.Input;
using DevelopmentInProgress.DipSecure;
using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.ViewModel;

namespace DevelopmentInProgress.AuthorisationManager.ViewModel
{
    public class ConfigurationAuthorisationViewModel : DocumentViewModel
    {
        public ConfigurationAuthorisationViewModel(ViewModelContext viewModelContext)
            : base(viewModelContext)
        {
        }

        public ICommand NewUserCommand { get; set; }

        public ICommand NewRoleCommand { get; set; }

        public ICommand NewActivityCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ObservableCollection<Role> Roles { get; set; }

        public ObservableCollection<Activity> Activities { get; set; }

        public ObservableCollection<UserAuthorisation> Users { get; set; }

        protected override ProcessAsyncResult OnPublishedAsync(object data)
        {
            return base.OnPublishedAsync(data);
        }

        protected override void OnPublishedAsyncCompleted(ProcessAsyncResult processAsyncResult)
        {
            base.OnPublishedAsyncCompleted(processAsyncResult);
        }

        protected override ProcessAsyncResult SaveDocumentAsync()
        {
            return base.SaveDocumentAsync();
        }
    }
}
