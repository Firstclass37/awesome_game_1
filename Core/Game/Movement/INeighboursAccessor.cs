using My_awesome_character.Core.Game.Constants;
using System.Collections.Generic;

namespace My_awesome_character.Core.Game.Movement
{
    internal interface INeighboursAccessor
    {
        MapCell[] GetNeighboursOf(MapCell mapCell);

        Dictionary<MapCell, Direction> GetDirectedNeighboursOf(MapCell mapCell);
    }
}