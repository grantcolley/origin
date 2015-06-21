using System;
using DevelopmentInProgress.DipState;

namespace DevelopmentInProgress.RemediationProgramme.Model
{
    public class Adjustment : EntityBase
    {
        public Adjustment()
            : base(canComplete: HasAdjustmentAmount)
        {            
        }

        public decimal? AdjustmentAmount { get; set; }

        private static bool HasAdjustmentAmount(DipState.DipState state)
        {
            if (((Adjustment)state).AdjustmentAmount.HasValue)
            {
                var adjustmentDecision = state.Antecedent as AdjustmentDecision;
                var collateData = adjustmentDecision.Antecedent as CollateData;

                if ((((Adjustment) state).AdjustmentAmount.Value
                    + collateData.RedressAmount.Value) > 100)
                {
                    return true;
                }

                state.Log.Add(
                    new LogEntry("The sum of the redress amount and the adjustment amount must be greater than 100."));
            }
            else
            {
                state.Log.Add(
                    new LogEntry(String.Format("{0} requires an adjustment amount before it can be completed.", state.Name)));                
            }

            return false;
        }
    }
}
