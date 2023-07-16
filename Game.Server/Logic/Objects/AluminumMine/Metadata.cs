using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.AluminumMine.Creation;
using Game.Server.Models.Constants;

namespace Game.Server.Logic.Objects.AluminumMine
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly IArea2x2Getter _area2X2Getter;
        private readonly AluminumMineFactory _aluminumMineFactory;

        public Metadata(IArea2x2Getter area2X2Getter, AluminumMineFactory aluminumMineFactory)
        {
            _area2X2Getter = area2X2Getter;
            _aluminumMineFactory = aluminumMineFactory;
        }

        public string ObjectType => BuildingTypes.AluminumMine;

        public string Description => "Aluminum Mine";

        public Dictionary<int, int> BasePrice => new Dictionary<int, int> 
        {
            { ResourceType.Steel, 45 }
        };

        public IAreaGetter AreaGetter => _area2X2Getter;

        public ICreationRequirement CreationRequirement => new OnlyTypeRequirement(ResourceResourceTypes.Bauxite);

        public IGameObjectFactory GameObjectFactory => _aluminumMineFactory;
    }
}