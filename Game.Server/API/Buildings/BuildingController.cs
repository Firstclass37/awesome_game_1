using Game.Server.Logic.Objects._Core;
using Game.Server.Models.Constants;

namespace Game.Server.API.Buildings
{
    public record BuildingInfo(string BuildingType, string Description, Price[] Prices);

    public record Price(int resourceType, float count);

    internal class BuildingController : IBuildingController
    {
        private IGameObjectMetadataCollection _gameObjectMetadataCollection;

        public BuildingController(IGameObjectMetadataCollection gameObjectMetadataCollection)
        {
            _gameObjectMetadataCollection = gameObjectMetadataCollection;
        }

        public IReadOnlyCollection<BuildingInfo> GetBuildableList()
        {
            var all = BuildingTypes.List;
            return all
                .Except(new string[]
                {
                    BuildingTypes.Ground,
                    BuildingTypes.TrafficLigh
                })
            .Select(t => _gameObjectMetadataCollection.Get(t))
            .Select(m => new BuildingInfo(m.ObjectType, m.Description, m.BasePrice.Chunks.Select(p => new Price(p.ResourceId, p.Amout)).ToArray()))
            .ToArray();
        }
    }
}