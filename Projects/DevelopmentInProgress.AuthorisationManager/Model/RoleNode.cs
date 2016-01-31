using System.Collections.Generic;
using DevelopmentInProgress.DipSecure;

namespace DevelopmentInProgress.AuthorisationManager.Model
{
    public class RoleNode : EntityBase
    {
        public List<RoleNode> Roles { get; set; }

        public List<ActivityNode> Activities { get; set; }
    }
}
