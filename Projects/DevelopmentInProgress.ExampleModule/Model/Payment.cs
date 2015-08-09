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
        }

        public DateTime? PaymentDate { get; set; }

        private async Task<bool> HasPaymentDateAsync(State state)
        {
            if (((Payment)state).PaymentDate.HasValue)
            {
                await TaskRunner.DoAsyncStuff();

                return true;
            }

            state.Log.Add(
                new LogEntry(String.Format("{0} requires a payment date before it can be completed.", state.Name)));

            return false;
        }
    }
}
