using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DTO;
using WebApi.Models;

namespace WebApi.Service
{
    public interface IEmployeeMapper 
    {
        Employee EmployeeDtoToEmployee(EmployeeDTO employee, IEnumerable<Position> positions);
        EmployeeDTO EmployeeToEmployeeDto(Employee employee);
    }
}
