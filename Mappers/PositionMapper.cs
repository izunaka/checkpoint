using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Service;

namespace WebApi.Mappers
{
    public class PositionMapper : IPositionMapper
    {
        public string PositionToString(Position position)
        {
            if (position == null)
            {
                throw new ArgumentException("Некорректные входные данные.");
            }
            return position.Name;
        }

        public Position StringToPosition(string position, IEnumerable<Position> positions)
        {
            Position pos = positions.FirstOrDefault(p => p.Name == position);
            if (pos == null)
            {
                throw new ArgumentException("Некорректные входные данные.");
            }
            return pos;
        }
    }
}
