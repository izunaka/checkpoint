using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTO
{
    public class ShiftDTO
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
    }
}
