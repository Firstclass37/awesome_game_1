using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Weapons
{
    internal interface ITargetLocator
    {
        GameObjectAggregator FindTarget(GameObjectAggregator weaponOwner, Dictionary<Coordiante, List<GameObjectAggregator>> targetPool);
    }
}