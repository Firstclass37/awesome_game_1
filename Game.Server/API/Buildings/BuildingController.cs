using Game.Server.Logic.Core;
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

        public bool Available { get; init; }
    
    };

    public record Price(int resourceType, float count);

    internal class BuildingController : IBuildingController
    {
        private readonly IGameObjectMetadataCollection _gameObjectMetadataCollection;
        private readonly IGameObjectCreator _gameObjectCreator;
        private readonly ISpendingTransactionFactory _spendingTransactionFactory;
        private readonly IBuidlingPricing _buidlingPricing;
        private readonly IBuildingAvailability _buildingAvailability;

        public BuildingController(IGameObjectMetadataCollection gameObjectMetadataCollection, IGameObjectCreator gameObjectCreator, ISpendingTransactionFactory spendingTransactionFactory, IBuidlingPricing buidlingPricing, IBuildingAvailability buildingAvailability)
        {
            _gameObjectMetadataCollection = gameObjectMetadataCollection;
            _gameObjectCreator = gameObjectCreator;
            _spendingTransactionFactory = spendingTransactionFactory;
            _buidlingPricing = buidlingPricing;
            _buildingAvailability = buildingAvailability;
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
                Prices = _buidlingPricing.GetActualPriceFor(m).Chunks.Select(p => new Price(p.ResourceId, p.Amout)).ToArray(),
                Available = _buildingAvailability.IsAvailable(m)
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
            var price = _buidlingPricing.GetActualPriceFor(_gameObjectMetadataCollection.Get(buildingType));
            using var transaction = _spendingTransactionFactory.Create();
            transaction.Spend(price.Chunks);
            _gameObjectCreator.Create(buildingType, point, null);
            transaction.Commit();
        }
    }
}