using My_awesome_character.Core.Constatns;
using System;
using System.Linq;

namespace My_awesome_character.Core.Game.Movement
{
    internal class OnlyRoadNeighboursSelector : INeighboursSelector
    {
        private readonly INeighboursAccessor _neighboursAccessor;

        public OnlyRoadNeighboursSelector(INeighboursAccessor neighboursAccessor)
        {
            _neighboursAccessor = neighboursAccessor;
        }

        public MapCell[] GetNeighboursOf(MapCell mapCell)
        {
            return _neighboursAccessor.GetNeighboursOf(mapCell).Where(c => c.Tags.Contains(MapCellTags.Road)).ToArray();
        }
    }
}