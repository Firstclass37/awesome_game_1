using Game.Server.DataAccess;
using Game.Server.Logic.Maps;
using Game.Server.Models.Constants.Attributes;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;
using Game.Server.Models.Weapons;

namespace Game.Server.Logic.Weapons.Common
{
    internal class DistanceTargetLocator : ITargetLocator
    {
        private readonly IAreaCalculator _areaCalculator;
        private readonly IGameObjectAccessor _gameObjectAccessor;
        private readonly IGameObjectAgregatorRepository _gameObjectAgregatorRepository;
        private readonly Weapon _weapon;

        public DistanceTargetLocator(IAreaCalculator areaCalculator, IGameObjectAccessor gameObjectAccessor,
            IGameObjectAgregatorRepository gameObjectAgregatorRepository, Weapon weapon)
        {
            _areaCalculator = areaCalculator;
            _gameObjectAccessor = gameObjectAccessor;
            _gameObjectAgregatorRepository = gameObjectAgregatorRepository;
            _weapon = weapon;
        }

        public GameObjectAggregator FindTarget(GameObjectAggregator weaponOwner, Dictionary<Coordiante, List<GameObjectAggregator>> targetPool)
        {
            var damageArea = _areaCalculator.GetAreaCross(weaponOwner.RootCell, _weapon.Distance);
            var lastTargetId = weaponOwner.GetAttributeValue(AttackAttributes.LastTarget);
            if (lastTargetId != null)
            {
                var lastTarget = _gameObjectAccessor.Get(lastTargetId.Value);
                if (lastTarget == null)
                {
                    weaponOwner.SetAttributeValue(AttackAttributes.LastTarget, null);
                    _gameObjectAgregatorRepository.Update(weaponOwner);
                }
                else if (damageArea.Contains(lastTarget.RootCell))
                    return lastTarget;
            }

            return damageArea
                .Where(a => targetPool.ContainsKey(a))
                .SelectMany(a => targetPool[a])
                .FirstOrDefault(c => c.GameObject.PlayerId != weaponOwner.GameObject.PlayerId);
        }
    }
}