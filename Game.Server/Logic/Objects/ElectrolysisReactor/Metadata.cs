using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.ElectrolysisReactor.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.ElectrolysisReactor
{
    internal class Metadata : IGameObjectMetadata
    {
        private readonly ElectrolysisReactorFactory _electrolysisReactorFactory;
        private readonly IArea2x2Getter _area2x2Getter;

        public Metadata(ElectrolysisReactorFactory electrolysisReactorFactory, IArea2x2Getter area2x2Getter)
        {
            _electrolysisReactorFactory = electrolysisReactorFactory;
            _area2x2Getter = area2x2Getter;
        }

        public string ObjectType => BuildingTypes.ElectrolysisReactor;

        public string Description => "Electrolysis Reactor";

        public IAreaGetter AreaGetter => _area2x2Getter;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => _electrolysisReactorFactory;

        public Price BasePrice => Price.Create(
            new ResourceChunk(ResourceType.Steel, 30), 
            new ResourceChunk(ResourceType.Coal, 10));
    }
}