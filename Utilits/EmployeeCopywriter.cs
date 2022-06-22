using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Service;

namespace WebApi.Utilits
{
    public class EmployeeCopywriter : IEmployeeCopywriter
    {
        public void Copy(Employee from, Employee to)
        {
            if (from.LastName != null)
            {
                to.LastName = from.LastName;
            }

            if (from.FirstName != null)
            {
                to.FirstName = from.FirstName;
            }

            if (from.MiddleName != null)
            {
                to.MiddleName = from.MiddleName;
            }

            if (from.Position != null)
            {
                to.Position = from.Position;
            }
        }
    }
}
