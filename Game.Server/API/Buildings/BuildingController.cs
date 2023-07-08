using Game.Server.Models.Constants;

namespace Game.Server.API.Buildings
{
    public record BuildingInfo(string Name, string Description);

    internal class BuildingController : IBuildingController
    {
        public IReadOnlyCollection<BuildingInfo> GetList()
        {
            return new BuildingInfo[]
            {
                new BuildingInfo(BuildingTypes.HomeType1, "Super home"),
                new BuildingInfo(BuildingTypes.PowerStation, "Power station"),
                new BuildingInfo(BuildingTypes.MineUranus, "Uranus mine"),
                new BuildingInfo(BuildingTypes.Road, "Road"),
            };
        }
    }
}