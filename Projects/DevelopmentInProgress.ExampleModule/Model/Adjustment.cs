using System;
using System.Threading.Tasks;
using DevelopmentInProgress.DipState;
using DevelopmentInProgress.ExampleModule.Service;

namespace DevelopmentInProgress.ExampleModule.Model
{
    public class Adjustment : EntityBase
    {
        private bool? isAdjustmentApplicable;

        public Adjustment()
        {
            this.AddCanCompletePredicateAsync(HasAdjustmentAmountAsync);
        }

        public decimal? AdjustmentAmount { get; set; }

        public bool? IsAdjustmentApplicable
        {
            get
            {
                if (isAdjustmentApplicable == null
                    && Status.Equals(StateStatus.Uninitialise))
                {
                    return null;
                }

                return isAdjustmentApplicable;
            }
            set
            {
                isAdjustmentApplicable = value;
            }
        }

        public bool IsArrowVisible
        {
            get
            {
                if (IsAdjustmentApplicable == null)
                {
                    return false;
                }

                return !IsAdjustmentApplicable.Value;
            }
        }

        private async Task<bool> HasAdjustmentAmountAsync(State state)
        {
            if (((Adjustment)state).AdjustmentAmount.HasValue)
            {
                var adjustmentDecision = state.Antecedent as AdjustmentDecision;
                var collateData = adjustmentDecision.Antecedent as CollateData;

                if ((((Adjustment) state).AdjustmentAmount.Value
                    + collateData.RedressAmount.Value) > 100)
                {
                    await TaskRunner.DoAsyncStuff();

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
