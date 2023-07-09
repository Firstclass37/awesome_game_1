namespace Game.Server.API.Buildings
{
    public interface IBuildingController
    {
        IReadOnlyCollection<BuildingInfo> GetBuildableList();
    }
}