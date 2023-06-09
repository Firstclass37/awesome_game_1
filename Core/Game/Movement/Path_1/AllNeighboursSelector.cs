﻿using My_awesome_character.Core.Constatns;
using My_awesome_character.Core.Game.Constants;
using My_awesome_character.Core.Game.Movement.Path;
using System.Linq;

namespace My_awesome_character.Core.Game.Movement.Path_1
{
    internal class AllNeighboursSelector : INieighborsSearchStrategy<MapCell>
    {
        private readonly INeighboursAccessor _neighboursAccessor;

        public AllNeighboursSelector(INeighboursAccessor neighboursAccessor)
        {
            _neighboursAccessor = neighboursAccessor;
        }

        public MapCell[] Search(MapCell element)
        {
            return _neighboursAccessor.GetNeighboursOf(element)
                .Where(c => c.CellType == MapCellType.Groud || c.CellType == MapCellType.Road || c.CellType == MapCellType.Resource || c.Tags.Contains(MapCellTags.Trap)).ToArray();
        }
    }
}