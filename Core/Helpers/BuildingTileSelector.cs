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
            { BuildingTypes.Road, Tiles.RoadAshpalt },
        };

        public int Select(BuildingTypes from)
        {
            return _tiles.ContainsKey(from) ? _tiles[from] : throw new ArgumentOutOfRangeException($"no tiles for type {from}");
        }
    }

    internal class NewBuildingTileSelector : ISelector<string, int>
    {
        private static readonly Dictionary<string, int> _tiles = new Dictionary<string, int>()
        {
            { BuildingTypesTrue.Home, Tiles.HomeType1 },
            { BuildingTypesTrue.UranusMine, Tiles.MineUranus },
            { BuildingTypesTrue.PowerStation, Tiles.PowerStation },
            { BuildingTypesTrue.Road, Tiles.RoadAshpalt },
            { BuildingTypesTrue.SolarBattery, Tiles.SolarBatary },
        };

        public int Select(string from)
        {
            return _tiles.ContainsKey(from) ? _tiles[from] : throw new ArgumentOutOfRangeException($"no tiles for type {from}");
        }
    }
}