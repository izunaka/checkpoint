using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTO
{
    public class ErrorResponseDTO
    {
        [Required]
        public int Code { get; set; }

        public string Message { get; set; }
    }
}
