using Game.Server.Models.Constants;
using Game.Server.Models.Maps;

namespace Game.Server.Models.Buildings
{
    internal interface IBuilding
    {
        public int Id { get; }

        public BuildingTypes BuildingType { get; }

        public Coordiante RootCell { get; }

        public Coordiante[] Cells { get; }
    }
}