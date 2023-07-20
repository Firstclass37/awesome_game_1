using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Models.Constants;
using Game.Server.Models.Maps;

namespace Game.Server.API.Buildings
{
    public record BuildingInfo(string BuildingType, string Description, Price[] Prices);

    public record Price(int resourceType, float count);

    internal class BuildingController : IBuildingController
    {
        private readonly IGameObjectMetadataCollection _gameObjectMetadataCollection;
        private readonly IGameObjectCreator _gameObjectCreator;

        public BuildingController(IGameObjectMetadataCollection gameObjectMetadataCollection, IGameObjectCreator gameObjectCreator)
        {
            _gameObjectMetadataCollection = gameObjectMetadataCollection;
            _gameObjectCreator = gameObjectCreator;
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


        public bool CanBuild(string buildingType, Coordiante point)
        {
            return _gameObjectCreator.CanCreate(buildingType, point, null);
        }
    }
}