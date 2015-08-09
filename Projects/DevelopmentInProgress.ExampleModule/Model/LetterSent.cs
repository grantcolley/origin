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
        }

        public DateTime? LetterSentDate { get; set; }

        private async Task<bool> HasLetterSentDateAsync(State state)
        {
            if (((LetterSent)state).LetterSentDate.HasValue)
            {
                await TaskRunner.DoAsyncStuff();

                return true;
            }

            state.WriteLogEntry(String.Format("{0} requires a letter sent date before it can be completed.", state.Name));

            return false;
        }
    }
}
