using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Core;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Logic.Objects.WindTurbine.Creation;
using Game.Server.Models.Constants;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects.WindTurbine
{
    internal class Metadata : IGameObjectMetadata
    {
        public string ObjectType => BuildingTypes.WindTurbine;

        public string Description => "Wind turbine";

        public AreaSize Size => AreaSize.Area1x1;

        public ICreationRequirement CreationRequirement => new OnlyGroundRequirement();

        public IGameObjectFactory GameObjectFactory => new WindTurbineFactory();

        public Price BasePrice => Price.Create(new ResourceChunk(ResourceType.Aluminum, 20), new ResourceChunk(ResourceType.Coal, 30));
    }
}