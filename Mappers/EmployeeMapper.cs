using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DTO;
using WebApi.Models;
using WebApi.Service;

namespace WebApi.Mappers
{
    public class EmployeeMapper : IEmployeeMapper
    {
        public Employee EmployeeDtoToEmployee(EmployeeDTO employee, IEnumerable<Position> positions)
        {
            Position position = positions.FirstOrDefault(p => p.Name == employee.Position);
            if (position == null && employee.Position != null)
            {
                throw new ArgumentException("Некорректные входные данные.");
            }

            Employee modelEmployee = new Employee()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                Position = position
            };

            if (employee.ID != null)
            {
                modelEmployee.ID = employee.ID.Value;
            }

            return modelEmployee;
        }

        public EmployeeDTO EmployeeToEmployeeDto(Employee employee)
        {
            return new EmployeeDTO()
            {
                ID = employee.ID,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                Position = employee.Position.Name
            };
        }
    }
}
