using System;
using System.Threading.Tasks;
using DevelopmentInProgress.DipState;
using DevelopmentInProgress.ExampleModule.Service;

namespace DevelopmentInProgress.ExampleModule.Model
{
    public class Adjustment : EntityBase
    {
        public Adjustment()
        {
            this.AddCanCompletePredicateAsync(HasAdjustmentAmountAsync);
        }

        public decimal? AdjustmentAmount { get; set; }

        private async Task<bool> HasAdjustmentAmountAsync(State state)
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

            await TaskRunner.DoAsyncStuff();

            return false;
        }
    }
}
