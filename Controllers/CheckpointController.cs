using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DTO;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    public class CheckpointController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CheckpointController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("api/[controller]/StartShift")]
        public IActionResult StartShift(ShiftDTO model)
        {
            try
            {
                Employee employee = _db.Employees
                    .Include(e => e.Position)
                    .Include(e => e.Shifts)
                    .FirstOrDefault(e => e.ID == model.ID);
                if (employee == null)
                {
                    throw new Exception("Сотрудник не найден.");
                }

                Shift shift = employee.Shifts.OrderByDescending(s => s.ShiftStart).FirstOrDefault();
                bool shiftValidate = shift == null || shift.ShiftEnd != null;
                if (!shiftValidate)
                {
                    throw new Exception("Есть незакрытая смена.");
                }

                employee.Shifts.Add(new Shift()
                {
                    Employee = employee,
                    ShiftStart = model.DateTime
                });
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

        [HttpPost]
        [Route("api/[controller]/EndShift")]
        public IActionResult EndShift(ShiftDTO model)
        {
            try
            {
                Employee employee = _db.Employees
                    .Include(e => e.Position)
                    .Include(e => e.Shifts)
                    .FirstOrDefault(e => e.ID == model.ID);
                if (employee == null)
                {
                    throw new Exception("Сотрудник не найден");
                }

                Shift shift = employee.Shifts.OrderByDescending(s => s.ShiftStart).FirstOrDefault();
                bool shiftValidate = shift != null && shift.ShiftEnd == null;
                if (!shiftValidate)
                {
                    throw new Exception("Нет открытой смены.");
                }

                shift.ShiftEnd = model.DateTime;
                shift.Hours = Convert.ToInt32((shift.ShiftEnd.Value.Ticks - shift.ShiftStart.Ticks) / 36000000000);
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
    }
}
