using System.Collections.Generic;

namespace DevelopmentInProgress.AuthorisationManager.Model
{
    public class UserNode : EntityBase
    {
        public List<RoleNode> Roles { get; set; }
    }
}
