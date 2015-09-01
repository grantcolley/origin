using System;
using System.Text;
using System.Threading.Tasks;
using DevelopmentInProgress.DipState;
using DevelopmentInProgress.ExampleModule.Service;

namespace DevelopmentInProgress.ExampleModule.Model
{
    public class AdjustmentDecision : State
    {
        public AdjustmentDecision()
        {
            this.AddActionAsync(StateActionType.OnEntry, ConditionalTransitionDecisionAsync);
            this.AddActionAsync(StateActionType.Reset, ResetAsync);
        }

        public bool? AdjustmentApplicable { get; set; }

        internal async Task ConditionalTransitionDecisionAsync(State context)
        {
            var collateData = context.Antecedent as CollateData;
            if (collateData.RedressAmount == null
                || collateData.RedressAmount.Value < 1000)
            {
                // If the calculated redress amount is less
                // than 100 transition to adjustment.
                context.Transition = context.Transitions[0];
                ((Adjustment)context.Transition).IsAdjustmentApplicable = true;
            }
            else
            {
                // If the calculated redress amount is greater 
                // or equal to 100 transition to redress review.
                context.Transition = context.Transitions[1];
                ((Adjustment) context.Transitions[0]).IsAdjustmentApplicable = false;
            }

            await TaskRunner.DoAsyncStuff();
        }

        public async Task ResetAsync(State state)
        {
            ((Adjustment) state.Transitions[0]).IsAdjustmentApplicable = null;
            ((Adjustment)state.Transitions[0]).AdjustmentAmount = null;
            ((Adjustment)state.Transitions[0]).OnPropertyChanged(String.Empty);
        }
    }
}
