using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Maps
{
    internal interface IGameObjectAccessor
    {
        GameObjectAggregator Get(Guid id);

        IEnumerable<GameObjectAggregator> FindAll(Coordiante position);

        GameObjectAggregator Find(Coordiante position);

        IReadOnlyCollection<GameObjectAggregator> FindAll(string gameObjectType);

        //todo: добавить поиск по атрибутам (+ индекс по ним)
    }
}