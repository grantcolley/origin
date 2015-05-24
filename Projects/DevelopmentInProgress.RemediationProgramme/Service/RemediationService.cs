using System.Collections.Generic;
using DevelopmentInProgress.RemediationProgramme.Model;

namespace DevelopmentInProgress.RemediationProgramme.Service
{
    public class RemediationService
    {
        public IEnumerable<Customer> GetCustomers()
        {
            return Data.GetCustomers();
        }
    }
}