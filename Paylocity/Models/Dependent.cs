using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Paylocity.Models
{
    public class Dependent
    {
        [Key]
        [Required]
        public int DependentID { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "First Name must be beteween 1 and 100 characters.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Last Name must be beteween 1 and 100 characters.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Employee is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Employee is required.")]
        public int EmployeeID { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
