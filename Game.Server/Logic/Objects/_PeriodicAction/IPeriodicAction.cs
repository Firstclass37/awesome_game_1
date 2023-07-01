using Game.Server.Models.GameObjects;

namespace Game.Server.Logic.Objects._PeriodicAction
{
    internal interface IPeriodicAction
    {
        void Trigger(GameObjectAggregator gameObject);
    }
}