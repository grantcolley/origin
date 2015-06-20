using System;
using System.Collections.Generic;
using System.Windows.Input;
using DevelopmentInProgress.DipState;
using DevelopmentInProgress.Origin.Messages;
using DevelopmentInProgress.RemediationProgramme.Model;
using DevelopmentInProgress.RemediationProgramme.Service;
using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.ViewModel;

namespace DevelopmentInProgress.RemediationProgramme.ViewModel
{
    public class CustomerRemediationViewModel : DocumentViewModel
    {
        private readonly RemediationService remediationService;

        public CustomerRemediationViewModel(ViewModelContext viewModelContext, RemediationService remediationService)
            : base(viewModelContext)
        {
            this.remediationService = remediationService;
            CompleteCommand = new ViewModelCommand(Complete);
            FailCommand = new ViewModelCommand(Fail);

            Products = new List<string>() {"Cap", "Collar", "Structured Collar"};
        }

        public ICommand CompleteCommand { get; set; }
        public ICommand FailCommand { get; set; }
        public List<Customer> Customers { get; set; }
        public Customer CurrentCustomer { get; set; }
        public List<string> Products { get; set; }

        protected override ProcessAsyncResult OnPublishedAsync()
        {
            Customers = remediationService.GetCustomers();
            return base.OnPublishedAsync();
        }

        private void Complete(object param)
        {
            var state = param as DipState.DipState;
            try
            {
                remediationService.Run(state, DipStateStatus.Completed);
            }
            catch (DipStateException e)
            {
                ShowMessage(new Message() {MessageType = MessageTypeEnum.Warn, Text = e.Message});
            }

            var entityBase = param as EntityBase;
            entityBase.OnPropertyChanged(String.Empty);

            CurrentCustomer.OnPropertyChanged("RemediationWorkflow");
        }

        private void Fail(object param)
        {
            var state = param as DipState.DipState;
            remediationService.Run(state, DipStateStatus.Failed);

            CurrentCustomer.OnPropertyChanged("RemediationWorkflow");
        }
    }
}
