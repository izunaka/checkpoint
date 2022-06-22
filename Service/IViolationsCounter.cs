using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Service
{
    public interface IViolationsCounter
    {
        int GetViolationsCount(IEnumerable<Shift> shifts, DateTime shiftStart, DateTime shiftEnd);
    }
}
