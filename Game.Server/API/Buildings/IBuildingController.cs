using Game.Server.Models.Maps;

namespace Game.Server.API.Buildings
{
    public interface IBuildingController
    {
        void Build(string buildingType, Coordiante point);
        bool CanBuild(string buildingType, Coordiante point);
        IReadOnlyCollection<BuildingInfo> GetBuildableList();
    }
}