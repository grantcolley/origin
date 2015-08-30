using System;
using System.Threading.Tasks;
using DevelopmentInProgress.DipState;
using DevelopmentInProgress.ExampleModule.Service;

namespace DevelopmentInProgress.ExampleModule.Model
{
    public class LetterSent : EntityBase
    {
        public LetterSent()
        {
            this.AddCanCompletePredicateAsync(HasLetterSentDateAsync);
            this.AddActionAsync(StateActionType.OnStatusChanged, RefreshAsync);
        }

        public DateTime? LetterSentDate { get; set; }

        private async Task<bool> HasLetterSentDateAsync(State state)
        {
            if (((LetterSent)state).LetterSentDate.HasValue)
            {
                await TaskRunner.DoAsyncStuff();

                return true;
            }

            var error = String.Format("{0} requires a letter sent date before it can be completed.", state.Name);
            state.WriteLogEntry(error);
            throw new StateException(state, error);
        }
    }
}
