using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;

namespace DevelopmentInProgress.AuthorisationManager.Model
{
    public class UserNode : EntityBase
    {
        public UserNode()
        {
            Roles = new List<RoleNode>();
        }

        public List<RoleNode> Roles { get; set; }
    }
}
