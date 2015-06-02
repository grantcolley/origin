using System;

namespace DevelopmentInProgress.RemediationProgramme.Model
{
    public class Communication : EntityBase
    {
        public DateTime? LetterSent { get; set; }
        public DateTime? ResponseReceived { get; set; }
        public decimal? ConsequentialLossClaim { get; set; }
    }
}
