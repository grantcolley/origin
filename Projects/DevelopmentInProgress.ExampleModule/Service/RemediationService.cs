using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevelopmentInProgress.DipState;
using DevelopmentInProgress.ExampleModule.Model;

namespace DevelopmentInProgress.ExampleModule.Service
{
    public class RemediationService
    {
        public async Task<State> CompleteStateAsync(State state)
        {
            return await state.ExecuteAsync(StateExecutionType.Complete);
        }

        public async Task<State> FailToCollateData(State redressReview)
        {
            var collateData = redressReview.Transitions.FirstOrDefault(t => t.Name.Equals("Collate Data"));
            return await redressReview.ExecuteAsync(collateData, true);
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            var customers = Data.GetCustomers();
            foreach (var customer in customers)
            {
                customer.RemediationWorkflow = await GetRemediationWorkflow();
            }

            return await Task.Run(() =>
            {
                Thread.Sleep(750);
                return customers;
            });
        }

        private async Task<List<State>> GetRemediationWorkflow()
        {
            var remediationWorkflow = new State(100, "Remediation Workflow", StateType.Root);

            var communication = new Communication()
            {
                Id = 200,
                Name = "Communication"
            };

            var letterSent = new LetterSent()
            {
                Id = 210,
                Name = "Letter Sent"
            };

            var responseReceived = new ResponseReceived()
            {
                Id = 220,
                Name = "Response Received"
            };

            var collateData = new CollateData()
            {
                Id = 300,
                Name = "Collate Data"
            };

            var adjustmentDecision = new AdjustmentDecision()
            {
                Id = 400,
                Name = "Adjustment Decision",
                Type = StateType.Auto
            };

            var adjustment = new Adjustment()
            {
                Id = 500,
                Name = "Adjustment"
            };

            var autoTransitionToRedressReview = new AutoTransitionToRedressReview()
            {
                Id = 600,
                Name = "Auto Transition Redress Review",
                Type = StateType.Auto
            };

            var redressReview = new RedressReview()
            {
                Id = 700,
                Name = "Redress Review"
            };

            var payment = new Payment()
            {
                Id = 800,
                Name = "Payment"
            };

            redressReview
                .AddTransition(payment, true)
                .AddTransition(collateData)
                .AddDependency(communication, true)
                .AddDependency(autoTransitionToRedressReview, true);

            autoTransitionToRedressReview
                .AddDependant(redressReview)
                .AddTransition(redressReview, true);

            adjustment
                .AddTransition(autoTransitionToRedressReview, true);

            adjustmentDecision
                .AddTransition(adjustment)
                .AddTransition(autoTransitionToRedressReview);

            collateData
                .AddTransition(adjustmentDecision, true);

            letterSent
                .AddTransition(responseReceived, true);

            communication
                .AddSubState(letterSent, true)
                .AddSubState(responseReceived)
                .AddDependant(redressReview)
                .AddTransition(redressReview, true);

            remediationWorkflow
                .AddSubState(communication, true)
                .AddSubState(collateData, true)
                .AddSubState(adjustmentDecision)
                .AddSubState(adjustment, completionRequired: false)
                .AddSubState(autoTransitionToRedressReview)
                .AddSubState(redressReview)
                .AddSubState(payment);

            await remediationWorkflow.ExecuteAsync(StateExecutionType.Initialise);

            return remediationWorkflow.Flatten();
        }
    }
}