using System;
using System.Collections.Generic;
using System.Linq;
using DevelopmentInProgress.DipState;
using DevelopmentInProgress.RemediationProgramme.Model;

namespace DevelopmentInProgress.RemediationProgramme.Service
{
    public class RemediationService
    {
        private readonly DipStateEngine dipStateEngine;

        public RemediationService(DipStateEngine dipStateEngine)
        {
            this.dipStateEngine = dipStateEngine;
        }

        public void Run(DipState.DipState state, DipStateStatus newStatus)
        {
            if (state.Name.Equals("Redress Review"))
            {
                if (newStatus.Equals(DipStateStatus.Failed))
                {
                    dipStateEngine.Run(state, newStatus,
                        state.Transitions.FirstOrDefault(t => t.Name.Equals("Collate Date")));
                }
                else if (newStatus.Equals(DipStateStatus.Completed))
                {
                    dipStateEngine.Run(state, newStatus,
                        state.Transitions.FirstOrDefault(t => t.Name.Equals("Payment")));
                }
            }
            else
            {
                dipStateEngine.Run(state, newStatus);
            }
        }

        public List<Customer> GetCustomers()
        {
            var customers = Data.GetCustomers();
            customers.ForEach(c => c.RemediationWorkflow = GetRemediationWorkflow());
            return customers;
        }

        private List<DipState.DipState> GetRemediationWorkflow()
        {
            var remediationWorkflow = new DipState.DipState(1, "Remediation Workflow", type: DipStateType.Root);
            var communication = new Communication() {Id = 2, Name = "Communication", InitialiseWithParent = true};
            var collateData = new CollateData() { Id = 3, Name = "Collate Date", InitialiseWithParent = true };
            var adjustmentDecision = new AdjustmentDecision() { Id = 4, Name = "Adjustment Decision", Type = DipStateType.Auto };
            var adjustment = new Adjustment() {Id = 5, Name = "Adjustment"};
            var autoTransitionRedressReview = new AutoTransitionRedressReview() { Id = 6, Name = "Auto Transition Redress Review", Type = DipStateType.Auto };
            var redressReview = new RedressReview() {Id = 6, Name = "Redress Review"};
            var payment = new Payment() { Id = 7, Name = "Payment", CanCompleteParent = true };

            remediationWorkflow
                .AddSubState(communication)
                .AddSubState(collateData)
                .AddSubState(adjustmentDecision)
                .AddSubState(adjustment)
                .AddSubState(autoTransitionRedressReview)
                .AddSubState(redressReview)
                .AddSubState(payment);

            communication.AddDependant(redressReview, true);

            collateData
                .AddTransition(adjustmentDecision);

            adjustmentDecision
                .AddTransition(adjustment)
                .AddTransition(autoTransitionRedressReview)
                .AddAction(DipStateActionType.Entry, (s => { s.Transition = s.Transitions[new Random().Next(0, 2)]; }));

            adjustment.AddTransition(autoTransitionRedressReview);

            autoTransitionRedressReview
                .AddTransition(redressReview)
                .AddDependant(redressReview);
            
            redressReview
                .AddTransition(payment)
                .AddTransition(collateData)
                .AddDependency(communication);

            remediationWorkflow = dipStateEngine.Run(remediationWorkflow, DipStateStatus.Initialised);
            return remediationWorkflow.Flatten();
        }
    }
}