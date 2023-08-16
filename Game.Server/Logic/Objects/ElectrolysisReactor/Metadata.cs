using Game.Server.Logic.Maps;
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
        public string ObjectType => BuildingTypes.ElectrolysisReactor;

        public string Description => "Electrolysis Reactor";

        public AreaSize Size => AreaSize.Area2x2;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => new ElectrolysisReactorFactory();

        public Price BasePrice => Price.Create(
            new ResourceChunk(ResourceType.Steel, 30), 
            new ResourceChunk(ResourceType.Coal, 10));
    }
}