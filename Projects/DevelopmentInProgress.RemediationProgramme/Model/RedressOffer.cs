using System;

namespace DevelopmentInProgress.RemediationProgramme.Model
{
    public class CustomerRemediation
    {
        // Client Communication
        public DateTime? LetterSent { get; set; }
        public DateTime? ResponseReceived { get; set; }
        public decimal? ConsequentialLossClaim { get; set; }

        // Data capture
        public string HedgingProduct { get; set; }
        public decimal? NominalAmount { get; set; }
        public decimal? Interest { get; set; }
        public DateTime? StartDate { get; set; }
        public decimal? RedressRate { get; set; }
        public decimal? RedressAmount { get; set; }

        // Offset Applicable
        public bool? OffsetApplicable { get; set; }

        // Apply Offset
        public decimal? OffsetAmount { get; set; }

        // Redress Review
        public decimal? FinalRedressAmount { get; set; }

        // Payment Executed
        public DateTime? PaymentExecuted { get; set; }
    }
}
