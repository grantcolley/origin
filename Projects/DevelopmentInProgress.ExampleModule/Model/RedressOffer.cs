using System;

namespace DevelopmentInProgress.ExampleModule.Model
{
    public class RedressOffer
    {
        public string HedgingProduct { get; set; }
        public decimal? TotalPaymentAmount { get; set; }
        public decimal? ConsequentialLoss { get; set; }
        public decimal? Interest { get; set; }
        public DateTime? StartDate { get; set; }
        public decimal? TotalRefund { get; set; }
        public bool OffsetApplicable { get; set; }
        public decimal? OffsetAmount { get; set; }
        public decimal? FinalOffer { get; set; }
    }
}
