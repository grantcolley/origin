using System;
using DevelopmentInProgress.DipState;

namespace DevelopmentInProgress.RemediationProgramme.Model
{
    public class Communication : EntityBase
    {
        public Communication()
            : base(canComplete: HasLetterSentDate)
        {            
        }

        public DateTime? LetterSent { get; set; }
        public DateTime? ResponseReceived { get; set; }
        public decimal? ConsequentialLossClaim { get; set; }

        private static bool HasLetterSentDate(DipState.DipState state)
        {
            if (((Communication)state).LetterSent.HasValue)
            {
                return true;
            }

            state.Log.Add(
                new LogEntry(String.Format("{0} requires a letter sent date before it can be completed.", state.Name)));

            return false;
        }
    }
}
