using System.Collections.Generic;
using System.Linq;
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
        }

        public List<Customer> Customers { get; set; }

        protected override ProcessAsyncResult OnPublishedAsync()
        {
            Customers = remediationService.GetCustomers().ToList();
            return base.OnPublishedAsync();
        }
    }
}
