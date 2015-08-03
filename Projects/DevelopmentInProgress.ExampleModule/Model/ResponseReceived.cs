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
        }

        public DateTime? ResponseReceivedDate { get; set; }
        public decimal? ConsequentialLossClaim { get; set; }

        private async Task<bool> HasResponseReceivedDateAsync(State state)
        {
            if (((ResponseReceived)state).ResponseReceivedDate.HasValue)
            {
                return true;
            }

            state.Log.Add(
                new LogEntry(String.Format("{0} requires a response recieved date before it can be completed.", state.Name)));

            await TaskRunner.DoAsyncStuff();

            return false;
        }
    }
}
