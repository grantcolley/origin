using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            dipStateEngine.Run(state, newStatus);
        }

        public void Run(DipState.DipState state, DipState.DipState transitionState)
        {
            dipStateEngine.Run(state, transitionState);
        }

        public List<Customer> GetCustomers()
        {
            var customers = Data.GetCustomers();
            customers.ForEach(c => c.RemediationWorkflow = GetRemediationWorkflow());
            return customers;
        }

        private List<DipState.DipState> GetRemediationWorkflow()
        {
            var communication = new Communication() {Id = 2, Name = "Communication", InitialiseWithParent = true};

            var payment = new Payment() {Id = 7, Name = "Payment", CanCompleteParent = true};

            var redressReview = new RedressReview() {Id = 6, Name = "Redress Review"}
                .AddTransition(payment)
                .AddDependency(communication);

            var adjustment = new Adjustment() {Id = 5, Name = "Adjustment"}
                .AddTransition(redressReview);

            var adjustmentDecision = new AdjustmentDecision()
            {
                Id = 4,
                Name = "Adjustment Decision",
                Type = DipStateType.Auto
            }
                .AddTransition(adjustment)
                .AddTransition(redressReview)
                .AddAction(DipStateActionType.Entry, (s => { s.Transition = s.Transitions[new Random().Next(0, 2)]; }));

            var collateData = new CollateData() {Id = 3, Name = "Collate Date", InitialiseWithParent = true}
                .AddTransition(adjustmentDecision);

            var remediationWorkflow = new DipState.DipState(1, "Remediation Workflow", type:DipStateType.Root)
                .AddSubState(communication)
                .AddSubState(collateData)
                .AddSubState(adjustmentDecision)
                .AddSubState(adjustment)
                .AddSubState(redressReview)
                .AddSubState(payment);

            remediationWorkflow = dipStateEngine.Run(remediationWorkflow, DipStateStatus.Initialised);
            return remediationWorkflow.Flatten();
        }
    }
}