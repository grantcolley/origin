using System;

namespace DevelopmentInProgress.RemediationProgramme.Model
{
    public class CollateData : DipState.DipState
    {
        public string HedgingProduct { get; set; }
        public decimal? NominalAmount { get; set; }
        public decimal? Interest { get; set; }
        public DateTime? StartDate { get; set; }
        public decimal? RedressRate { get; set; }
        public decimal? RedressAmount { get; set; }
    }
}
