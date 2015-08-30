using System;
using System.Threading.Tasks;
using DevelopmentInProgress.DipState;
using DevelopmentInProgress.ExampleModule.Service;

namespace DevelopmentInProgress.ExampleModule.Model
{
    public class ResponseReceived : EntityBase
    {
        public ResponseReceived()
        {
            this.AddCanCompletePredicateAsync(HasResponseReceivedDateAsync);
            this.AddActionAsync(StateActionType.OnStatusChanged, RefreshAsync);
        }

        public DateTime? ResponseReceivedDate { get; set; }
        public decimal? ConsequentialLossClaim { get; set; }

        private async Task<bool> HasResponseReceivedDateAsync(State state)
        {
            if (((ResponseReceived)state).ResponseReceivedDate.HasValue)
            {
                await TaskRunner.DoAsyncStuff();

                return true;
            }

            var error = String.Format("{0} requires a response recieved date before it can be completed.", state.Name);
            state.WriteLogEntry(error);
            throw new StateException(state, error);
        }
    }
}
