using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Maps
{
    internal interface IGameObjectAccessor
    {
        GameObjectAggregator Get(Guid id);

        GameObjectAggregator Find(Coordiante position);

        IReadOnlyCollection<GameObjectAggregator> FindAll(string gameObjectType);
    }
}