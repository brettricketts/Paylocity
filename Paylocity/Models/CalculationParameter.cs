using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Paylocity.Models
{
    public class CalculationParameter
    {
        [Key]
        public int CalculationParameterID { get; set; }
        public string Parameter { get; set; }
        public decimal Value { get; set; }
    }
}
