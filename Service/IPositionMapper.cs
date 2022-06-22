using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Service
{
    public interface IPositionMapper
    {
        string PositionToString(Position position);
        Position StringToPosition(string position, IEnumerable<Position> positions);
    }
}
