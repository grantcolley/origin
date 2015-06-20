using System;
using DevelopmentInProgress.DipState;

namespace DevelopmentInProgress.RemediationProgramme.Model
{
    public class CollateData : EntityBase
    {
        private decimal? nominalAmount;
        private decimal? interest;
        private DateTime? startDate;
        private decimal? redressRate;

        public CollateData()
            : base(canComplete: HasRedressRate)
        {            
        }

        public string HedgingProduct { get; set; }

        public decimal? RedressAmount
        {
            get
            {
                decimal? redressAmount = null;
                if (NominalAmount.HasValue
                    && Interest.HasValue)
                {
                    redressAmount = nominalAmount*(interest/100);
                }

                return redressAmount;
            }
        }

        public decimal? NominalAmount
        {
            get { return nominalAmount; }
            set
            {
                nominalAmount = value;
                OnPropertyChanged(String.Empty);
            }
        }

        public decimal? Interest
        {
            get { return interest; }
            set
            {
                interest = value;
                OnPropertyChanged(String.Empty);
            }
        }

        public DateTime? StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged(String.Empty);
            }
        }

        public decimal? RedressRate
        {
            get { return redressRate; }
            set
            {
                redressRate = value;
                OnPropertyChanged(String.Empty);
            }
        }

        private static bool HasRedressRate(DipState.DipState state)
        {
            if (((CollateData) state).RedressAmount.HasValue)
            {
                return true;
            }

            state.Log.Add(
                new LogEntry(String.Format("{0} requires a redress amount before it can be completed.", state.Name)));

            return false;
        }
    }
}
