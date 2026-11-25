using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paylocity.ViewModels
{
    public class SettingsVM
    {
        public decimal DiscountPercent { get; set; }
        public decimal EmployeeBenefits { get; set; }
        public decimal DependentBenefits { get; set; }
        public decimal PaycheckAmount { get; set; }
        public decimal PaycheckCount { get; set; }
    }
}
