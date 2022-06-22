using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DTO;
using WebApi.Models;
using WebApi.Service;

namespace WebApi.Controllers
{
    [ApiController]
    public class PersonnelDepartmentController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IEmployeeMapper _mapperEmployees;
        private readonly IPositionMapper _mapperPosition;
        private readonly IEmployeeCopywriter _copywriter;
        private readonly IViolationsCounter _counter;

        public PersonnelDepartmentController(AppDbContext db, 
            IEmployeeMapper mapperEmployees, IPositionMapper mapperPosition, 
            IEmployeeCopywriter copywriter, IViolationsCounter counter)
        {
            _db = db;
            _mapperEmployees = mapperEmployees;
            _mapperPosition = mapperPosition;
            _copywriter = copywriter;
            _counter = counter;
        }

        [HttpPost]
        [Route("api/[controller]/Create")]
        public async Task<object> Create(EmployeeDTO model)
        {
            try
            {
                Employee employee = _mapperEmployees.EmployeeDtoToEmployee(model, _db.Positions);
                Employee newEmployee = (await _db.Employees.AddAsync(employee)).Entity;
                _db.SaveChanges();
                EmployeeDTO result = _mapperEmployees.EmployeeToEmployeeDto(newEmployee);
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponseDTO()
                {
                    Code = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("api/[controller]/Update")]
        public object Update(EmployeeDTO model)
        {
            try
            {
                bool modelIsValid = model.ID != null && _db.Employees.FirstOrDefault(e => e.ID == model.ID) != null;
                if (!modelIsValid)
                {
                    throw new Exception("Сотрудник не найден.");
                }

                Employee newEmployee = _mapperEmployees.EmployeeDtoToEmployee(model, _db.Positions);
                Employee employee = _db.Employees.FirstOrDefault(e => e.ID == model.ID);
                _copywriter.Copy(newEmployee, employee);
                _db.SaveChanges();

                EmployeeDTO result = _mapperEmployees.EmployeeToEmployeeDto(employee);
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponseDTO()
                {
                    Code = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpDelete]
        [Route("api/[controller]/Delete")]
        public IActionResult Delete(EmployeeDTO model)
        {
            try
            {
                Employee employee = _mapperEmployees.EmployeeDtoToEmployee(model, _db.Positions);
                _db.Employees.Remove(employee);
                _db.SaveChanges();
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponseDTO()
                {
                    Code = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("api/[controller]/GetEmployees")]
        public async Task<object> GetEmployees(string pos = null)
        {
            try
            {
                Position position = null;
                if (pos != null)
                {
                    position = _db.Positions.FirstOrDefault(p => p.Name == pos);
                    if (position == null)
                    {
                        throw new Exception("Некорректные входные данные");
                    }
                }
                List<Employee> employees = await _db.Employees
                    .Include(e => e.Position)
                    .Include(e => e.Shifts)
                    .Where(e => pos == null || e.Position.Equals(position)).ToListAsync();

                List<EmployeeDTO> result = new List<EmployeeDTO>();
                DateTime month = DateTime.Now.AddMonths(-1);
                foreach (Employee employee in employees)
                {
                    EmployeeDTO temp = _mapperEmployees.EmployeeToEmployeeDto(employee);
                    temp.ViolationsCount = _counter.GetViolationsCount(
                        employee.Shifts.Where(s => s.ShiftStart > month),
                        employee.Position.ShiftStart,
                        employee.Position.ShiftEnd
                    );
                    result.Add(temp);
                }
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponseDTO()
                {
                    Code = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("api/[controller]/GetPositions")]
        public async Task<object> GetPositions()
        {
            try
            {
                List<Position> positions = _db.Positions.ToList();

                List<string> result = new List<string>();
                foreach (Position position in positions)
                {
                    result.Add(_mapperPosition.PositionToString(position));
                }
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponseDTO()
                {
                    Code = 400,
                    Message = ex.Message
                });
            }
        }
    }
}
