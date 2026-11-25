using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paylocity.ViewModels
{
    public class EmployeeCostsVM
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public bool? IsDependent { get; set; }
        public decimal? BenefitsCost { get; set; }
        public decimal? Pay { get; set; }
        public decimal? Total { get; set; }

    }
}
