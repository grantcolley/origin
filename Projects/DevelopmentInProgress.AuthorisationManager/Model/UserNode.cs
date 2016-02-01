using System.Collections.ObjectModel;

namespace DevelopmentInProgress.AuthorisationManager.Model
{
    public class UserNode : EntityBase
    {
        public UserNode()
        {
            Roles = new ObservableCollection<RoleNode>();
        }

        public ObservableCollection<RoleNode> Roles { get; set; }
    }
}
