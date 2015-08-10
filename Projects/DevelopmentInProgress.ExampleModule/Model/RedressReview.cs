using System.Linq;
using System.Threading.Tasks;
using DevelopmentInProgress.DipState;
using DevelopmentInProgress.ExampleModule.Service;

namespace DevelopmentInProgress.ExampleModule.Model
{
    public class RedressReview : EntityBase
    {
        public decimal? FinalRedressAmount { get; set; }

        internal async Task CalculateFinalRedressAmountAsync(State context)
        {
            var states = context.Flatten();
            var collateData = states.OfType<CollateData>().FirstOrDefault(s => s.Name.Equals("Collate Data"));
            var responseReceived = states.OfType<ResponseReceived>().FirstOrDefault(s => s.Name.Equals("Response Received"));
            var adjustment = states.OfType<Adjustment>().FirstOrDefault(s => s.Name.Equals("Adjustment"));

            decimal? finalRedressAmount = 0;

            if (collateData != null
                && collateData.RedressAmount.HasValue)
            {
                finalRedressAmount = collateData.RedressAmount;
            }

            if (adjustment != null
                && adjustment.AdjustmentAmount.HasValue)
            {
                finalRedressAmount += adjustment.AdjustmentAmount;
            }

            if (responseReceived != null
                && responseReceived.ConsequentialLossClaim.HasValue)
            {
                finalRedressAmount += responseReceived.ConsequentialLossClaim;
            }

            FinalRedressAmount = finalRedressAmount;

            await TaskRunner.DoAsyncStuff();
        }
    }
}
