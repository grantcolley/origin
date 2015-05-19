using System.Collections.Generic;
using DevelopmentInProgress.ExampleModule.Model;

namespace DevelopmentInProgress.ExampleModule.Service
{
    public class RemediationService
    {
        public IEnumerable<Customer> GetCustomers()
        {
            return Data.GetCustomers();
        }
    }
}