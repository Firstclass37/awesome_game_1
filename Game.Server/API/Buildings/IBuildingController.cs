namespace Game.Server.API.Buildings
{
    internal interface IBuildingController
    {
        IReadOnlyCollection<BuildingInfo> GetBuildableList();
    }
}