using System.Collections.Generic;
using System.Linq;
using DevelopmentInProgress.ExampleModule.Model;
using DevelopmentInProgress.ExampleModule.Service;
using DevelopmentInProgress.Origin.Context;
using DevelopmentInProgress.Origin.ViewModel;

namespace DevelopmentInProgress.ExampleModule.ViewModel
{
    public class RemediationWorkflowViewModel : DocumentViewModel
    {
        private readonly RemediationService remediationService;

        public RemediationWorkflowViewModel(ViewModelContext viewModelContext, RemediationService remediationService)
            : base(viewModelContext)
        {
            this.remediationService = remediationService;
        }

        public List<Customer> Customers { get; set; }

        protected override ProcessAsyncResult OnPublishedAsync()
        {
            Customers = remediationService.GetCustomers().ToList();
            return base.OnPublishedAsync();
        }
    }
}
