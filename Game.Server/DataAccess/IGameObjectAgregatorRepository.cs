using Game.Server.Models.GameObjects;

namespace Game.Server.DataAccess
{
    internal interface IGameObjectAgregatorRepository
    {
        void Add(GameObjectAggregator gameObjectAggregator);

        void Remove(GameObjectAggregator gameObjectAggregator);

        void Update(GameObjectAggregator gameObjectAggregator);
    }
}