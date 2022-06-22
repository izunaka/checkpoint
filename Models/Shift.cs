using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Shift
    {
        public int ID { get; set; }

        [Required]
        public DateTime ShiftStart { get; set; }

        public DateTime? ShiftEnd { get; set; }

        public int? Hours { get; set; }

        [Required]
        public Employee Employee { get; set; }

        public int EmployeeID { get; set; }
    }
}
