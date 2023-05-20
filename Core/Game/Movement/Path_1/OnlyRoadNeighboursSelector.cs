using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Movement.Path;
using System;
using System.Linq;

namespace My_awesome_character.Core.Game.Movement.Path_1
{
    internal class OnlyRoadNeighboursSelector : INieighborsSearchStrategy<MapCell>
    {
        private readonly INeighboursAccessor _neighboursAccessor;

        public OnlyRoadNeighboursSelector(INeighboursAccessor neighboursAccessor)
        {
            _neighboursAccessor = neighboursAccessor;
        }

        public MapCell[] Search(MapCell element)
        {
            return _neighboursAccessor.GetNeighboursOf(element).Where(c => c.CellType == MapCellType.Road).ToArray();
        }
    }
}