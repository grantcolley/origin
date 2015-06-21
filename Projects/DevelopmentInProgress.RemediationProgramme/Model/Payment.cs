using System;
using DevelopmentInProgress.DipState;

namespace DevelopmentInProgress.RemediationProgramme.Model
{
    public class Payment : EntityBase
    {
        public Payment()
            : base(canComplete: HasPaymentDate)
        {            
        }

        public DateTime? PaymentDate { get; set; }

        private static bool HasPaymentDate(DipState.DipState state)
        {
            if (((Payment)state).PaymentDate.HasValue)
            {
                return true;
            }

            state.Log.Add(
                new LogEntry(String.Format("{0} requires a payment date before it can be completed.", state.Name)));

            return false;
        }
    }
}
