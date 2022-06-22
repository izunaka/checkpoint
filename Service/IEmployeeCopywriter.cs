using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Service
{
    public interface IEmployeeCopywriter
    {
        void Copy(Employee from, Employee to);
    }
}
