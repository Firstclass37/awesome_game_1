using Game.Server.Models.GameObjects;

namespace Game.Server.Logic.Maps
{
    internal interface IGameObjectAccessor
    {
        GameObjectAggregator Get(Guid id);

        IReadOnlyCollection<GameObjectAggregator> FindAll(string gameObjectType);
    }
}