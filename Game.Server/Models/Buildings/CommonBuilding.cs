using Game.Server.Logic.Building;
using Game.Server.Models.Constants;
using Game.Server.Models.Maps;

namespace Game.Server.Models.Buildings
{
    internal class CommonBuilding : IBuilding
    {
        public int Id { get; internal set; }

        public BuildingTypes BuildingType { get; internal set; }

        public Coordiante RootCell { get; internal set; }

        public Coordiante[] Cells { get; internal set; }

        public IPeriodicAction PeriodicAction { get; internal set; }

        public IInteractionAction InteractionAction { get; internal set; }
    }
}