using System.Collections.Generic;

namespace DevelopmentInProgress.AuthorisationManager.Model
{
    public class ActivityNode : EntityBase
    {
        public List<ActivityNode> Activities { get; set; }
    }
}
