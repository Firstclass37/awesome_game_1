using Game.Server.Logic.Objects._Buidling;
using Game.Server.Logic.Objects._Requirements;

namespace Game.Server.Logic.Objects._Core
{
    internal interface IGameObjectMetadata
    {
        //todo: значений свойств должны меняться в зависимости от некого передаваемго контекста GameObjectCreationContext { ... }

        public string ObjectType { get; }

        public string Description { get; }

        public Dictionary<int, int> BasePrice { get; }

        public IAreaGetter AreaGetter { get; }

        public ICreationRequirement CreationRequirement { get; }

        public IGameObjectFactory GameObjectFactory { get; }
    }
}