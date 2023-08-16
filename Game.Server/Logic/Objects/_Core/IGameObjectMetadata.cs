using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Requirements;
using Game.Server.Models.Resources;

namespace Game.Server.Logic.Objects._Core
{
    internal interface IGameObjectMetadata
    {
        //todo: значений свойств должны меняться в зависимости от некого передаваемго контекста GameObjectCreationContext { ... }

        public string ObjectType { get; }

        public string Description { get; }

        public AreaSize Size { get; }

        public ICreationRequirement CreationRequirement { get; }

        public IGameObjectFactory GameObjectFactory { get; }

        public Price BasePrice { get; }
    }
}