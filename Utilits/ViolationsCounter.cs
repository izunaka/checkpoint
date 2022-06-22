using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Service;

namespace WebApi.Utilits
{
    public class ViolationsCounter : IViolationsCounter
    {
        public int GetViolationsCount(IEnumerable<Shift> shifts, DateTime shiftStart, DateTime shiftEnd)
        {
            int result = 0;
            foreach (Shift shift in shifts)
            {
                DateTime start = new DateTime(shiftStart.Year, shiftStart.Month, shiftStart.Day,
                    shift.ShiftStart.Hour, shift.ShiftStart.Minute, shift.ShiftStart.Second);
                if (start > shiftStart)
                {
                    result++;
                    continue;
                }
                if (shift.ShiftEnd != null)
                {
                    DateTime end = new DateTime(shiftEnd.Year, shiftEnd.Month, shiftEnd.Day,
                        shift.ShiftEnd.Value.Hour, shift.ShiftEnd.Value.Minute, shift.ShiftEnd.Value.Second);
                    if (end < shiftEnd)
                    {
                        result++;
                    }
                }
            }

            return result;
        }
    }
}
