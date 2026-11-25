using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Paylocity.ViewModels
{
    public class EmployeeVM
    {
        public int EmployeeID { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }
        public bool Active { get; set; }
        public string FullName => FirstName + " " + LastName;

    }
}
