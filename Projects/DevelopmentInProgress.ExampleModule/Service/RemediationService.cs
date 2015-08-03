using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevelopmentInProgress.DipState;
using DevelopmentInProgress.ExampleModule.Model;

namespace DevelopmentInProgress.ExampleModule.Service
{
    public class RemediationService
    {
        public void Run(State state, StateStatus newStatus)
        {
            if (state.Name.Equals("Redress Review"))
            {
                if (newStatus.Equals(StateStatus.Fail))
                {
                    state.Execute(newStatus,
                        state.Transitions.FirstOrDefault(t => t.Name.Equals("Collate Date")));
                }
                else if (newStatus.Equals(StateStatus.Complete))
                {
                    state.Execute(newStatus,
                        state.Transitions.FirstOrDefault(t => t.Name.Equals("Payment")));
                }
            }
            else
            {
                state.Execute(newStatus);
            }
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            var customers = Data.GetCustomers();
            foreach (var customer in customers)
            {
                customer.RemediationWorkflow = await GetRemediationWorkflow();
            }

            return customers;
        }

        private async Task<List<State>> GetRemediationWorkflow()
        {
            var remediationWorkflow = new State(100, "Remediation Workflow",
                type: StateType.Root);

            var communication = new Communication()
            {
                Id = 200,
                Name = "Communication",
                InitialiseWithParent = true
            };

            var letterSent = new LetterSent()
            {
                Id = 210,
                Name = "Letter Sent",
                InitialiseWithParent = true
            };

            var responseReceived = new ResponseReceived()
            {
                Id = 220,
                Name = "Response Received",
                CanCompleteParent = true
            };

            var collateData = new CollateData()
            {
                Id = 300,
                Name = "Collate Data",
                InitialiseWithParent = true
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
                Name = "Payment",
                CanCompleteParent = true
            };

            redressReview
                .AddTransition(payment)
                .AddTransition(collateData)
                .AddDependency(communication)
                .AddDependency(autoTransitionToRedressReview);

            autoTransitionToRedressReview
                .AddDependant(redressReview, true)
                .AddTransition(redressReview);

            adjustment.AddTransition(autoTransitionToRedressReview);

            adjustmentDecision
                .AddTransition(adjustment)
                .AddTransition(autoTransitionToRedressReview)
                .AddActionAsync(StateActionType.Entry, adjustmentDecision.ConditionalTransitionDecisionAsync);

            collateData
                .AddTransition(adjustmentDecision);

            letterSent.AddTransition(responseReceived);

            communication
                .AddDependant(redressReview, true)
                .AddSubState(letterSent)
                .AddSubState(responseReceived)
                .AddTransition(redressReview);
            
            remediationWorkflow
                .AddSubState(communication)
                .AddSubState(collateData)
                .AddSubState(adjustmentDecision)
                .AddSubState(adjustment)
                .AddSubState(autoTransitionToRedressReview)
                .AddSubState(redressReview)
                .AddSubState(payment);

            await remediationWorkflow.ExecuteAsync(StateStatus.Initialise);

            return remediationWorkflow.Flatten();
        }
    }
}