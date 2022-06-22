using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDataController : ControllerBase
    {
        private readonly AppDbContext _db;

        public TestDataController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<object> Post()
        {
            try
            {
                if (_db.Positions.Count() > 0)
                {
                    throw new Exception();
                }

                await _db.Positions.AddRangeAsync(
                    new Position()
                    {
                        Name = "Менеджер",
                        ShiftStart = new DateTime(1, 1, 1, 9, 0, 0),
                        ShiftEnd = new DateTime(1, 1, 1, 18, 0, 0)
                    },
                    new Position()
                    {
                        Name = "Инженер",
                        ShiftStart = new DateTime(1, 1, 1, 9, 0, 0),
                        ShiftEnd = new DateTime(1, 1, 1, 18, 0, 0)
                    },
                    new Position()
                    {
                        Name = "Тестировщик свечей",
                        ShiftStart = new DateTime(1, 1, 1, 9, 0, 0),
                        ShiftEnd = new DateTime(1, 1, 1, 21, 0, 0)
                    }
                );
                _db.SaveChanges();

                return _db.Positions.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
