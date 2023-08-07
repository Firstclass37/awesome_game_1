using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Resources;
using Game.Server.Models.Constants;
using Game.Server.Models.Maps;

namespace Game.Server.API.Buildings
{
    public record BuildingInfo
    {
        public string BuildingType { get; init; }

        public string Description { get; init; }

        public Price[] Prices { get; init; }

        public bool CanBuild { get; init; }
    
    };

    public record Price(int resourceType, float count);

    internal class BuildingController : IBuildingController
    {
        private readonly IGameObjectMetadataCollection _gameObjectMetadataCollection;
        private readonly IGameObjectCreator _gameObjectCreator;
        private readonly ISpendingTransactionFactory _spendingTransactionFactory;

        public BuildingController(IGameObjectMetadataCollection gameObjectMetadataCollection, IGameObjectCreator gameObjectCreator, ISpendingTransactionFactory spendingTransactionFactory)
        {
            _gameObjectMetadataCollection = gameObjectMetadataCollection;
            _gameObjectCreator = gameObjectCreator;
            _spendingTransactionFactory = spendingTransactionFactory;
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
            .Select(m => new BuildingInfo 
            {
                BuildingType = m.ObjectType,
                Description = m.Description,
                Prices = m.BasePrice.Chunks.Select(p => new Price(p.ResourceId, p.Amout)).ToArray()
            })
            .ToArray();
        }

        public bool CanBuild(string buildingType, Coordiante point)
        {
            return _gameObjectCreator.CanCreate(buildingType, point, null);
        }

        public void Build(string buildingType, Coordiante point) 
        {
            try
            {
                _gameObjectCreator.Create(buildingType, point, null);
            }
            catch (Exception e)
            {
                //
            }
        }

        public void Buy(string buildingType, Coordiante point)
        {
            var price = _gameObjectMetadataCollection.Get(buildingType);
            using var transaction = _spendingTransactionFactory.Create();
            transaction.Spend(price.BasePrice.Chunks);
            _gameObjectCreator.Create(buildingType, point, null);
            transaction.Commit();
        }
    }
}