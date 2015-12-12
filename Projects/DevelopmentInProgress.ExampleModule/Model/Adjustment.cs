using System;
using System.Linq;
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
            this.AddActionAsync(StateActionType.OnStatusChanged, RefreshAsync);
        }

        public decimal? AdjustmentAmount { get; set; }

        public bool? IsAdjustmentApplicable
        {
            get
            {
                if (isAdjustmentApplicable == null
                    && Status == StateStatus.Uninitialised)
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

        public override async Task RefreshAsync(State state)
        {
            OnPropertyChanged(String.Empty);
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
                    if (Transition == null)
                    {
                        Transition = Transitions.FirstOrDefault();
                    }

                    await TaskRunner.DoAsyncStuff();

                    return true;
                }

                var error = "The sum of the redress amount and the adjustment amount must be greater than 100.";
                state.Log.Add(new LogEntry(error));
                throw new StateException(state, error);
            }
            else
            {
                var error = String.Format("{0} requires an adjustment amount before it can be completed.", state.Name);
                state.WriteLogEntry(error);      
                throw new StateException(state, error);
            }
        }
    }
}
