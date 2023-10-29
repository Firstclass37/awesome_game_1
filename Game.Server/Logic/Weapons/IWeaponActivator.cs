using Game.Server.Models.GameObjects;

namespace Game.Server.Logic.Weapons
{
    internal interface IWeaponActivator
    {
        bool Activate(GameObjectAggregator who, GameObjectAggregator target, double gameTimeSeconds);
    }
}