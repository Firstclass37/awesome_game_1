using Game.Server.Models.GameObjects;

namespace Game.Server.Logic.Weapons
{
    internal interface IActivateCondition
    {
        bool CanUse(GameObjectAggregator who, GameObjectAggregator target, double gameTimeSeconds);
    }
}