using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.DTO
{
    public class EmployeeDTO
    {
        public int? ID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string Position { get; set; }

        public int? ViolationsCount { get; set; }
    }
}
