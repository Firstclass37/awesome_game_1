using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_awesome_character.Core.Game.Movement
{
    internal interface INeighboursAccessor
    {
        MapCell[] GetNeighboursOf(MapCell mapCell);
    }
}
