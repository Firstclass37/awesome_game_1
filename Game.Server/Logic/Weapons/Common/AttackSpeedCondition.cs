using Game.Server.Models.Constants.Attributes;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Weapons;

namespace Game.Server.Logic.Weapons.Common
{
    internal class AttackSpeedCondition : IActivateCondition
    {
        private readonly Weapon _weapon;

        public AttackSpeedCondition(Weapon weapon)
        {
            _weapon = weapon;
        }

        public bool CanUse(GameObjectAggregator who, GameObjectAggregator target, double gameTimeSeconds)
        {
            return gameTimeSeconds - who.GetAttributeValue(AttackAttributes.LastAttackTime) > _weapon.Speed;
        }
    }
}