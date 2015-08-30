using System;
using System.Threading.Tasks;
using DevelopmentInProgress.DipState;
using DevelopmentInProgress.ExampleModule.Service;

namespace DevelopmentInProgress.ExampleModule.Model
{
    public class Payment : EntityBase
    {
        public Payment()
        {
            this.AddCanCompletePredicateAsync(HasPaymentDateAsync);
            this.AddActionAsync(StateActionType.OnStatusChanged, RefreshAsync);
        }

        public DateTime? PaymentDate { get; set; }

        private async Task<bool> HasPaymentDateAsync(State state)
        {
            if (((Payment)state).PaymentDate.HasValue)
            {
                await TaskRunner.DoAsyncStuff();

                return true;
            }

            var error = String.Format("{0} requires a payment date before it can be completed.", state.Name);
            state.WriteLogEntry(error);
            throw new StateException(state, error);
        }
    }
}
