using System.Collections.ObjectModel;

namespace DevelopmentInProgress.AuthorisationManager.Model
{
    public class RoleNode : EntityBase
    {
        public RoleNode()
        {
            Roles = new ObservableCollection<RoleNode>();
            Activities= new ObservableCollection<ActivityNode>();
        }

        public ObservableCollection<RoleNode> Roles { get; set; }

        public ObservableCollection<ActivityNode> Activities { get; set; }
    }
}
