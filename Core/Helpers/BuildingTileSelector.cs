using My_awesome_character.Core.Constatns;
using System;
using System.Collections.Generic;

namespace My_awesome_character.Core.Helpers
{
    internal class BuildingTileSelector : ISelector<BuildingTypes, int>
    {
        private static readonly Dictionary<BuildingTypes, int> _tiles = new Dictionary<BuildingTypes, int>()
        {
            { BuildingTypes.HomeType1, Tiles.HomeType1 },
            { BuildingTypes.MineUranus, Tiles.MineUranus },
            { BuildingTypes.PowerStation, Tiles.PowerStation },
        };

        public int Select(BuildingTypes from)
        {
            return _tiles.ContainsKey(from) ? _tiles[from] : throw new ArgumentOutOfRangeException($"no tiles for type {from}");
        }
    }
}