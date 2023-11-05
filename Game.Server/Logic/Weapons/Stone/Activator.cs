using Game.Server.DataAccess;
using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Interactions;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Models.Constants;
using Game.Server.Models.Constants.Attributes;
using Game.Server.Models.GameObjects;
using Game.Server.Models.GamesObjectList;
using Game.Server.Models.Maps;
using Game.Server.Models.Weapons;

namespace Game.Server.Logic.Weapons.Stone
{
    internal class Activator : IWeaponActivator
    {
        private readonly IGameObjectAgregatorRepository _agregatorRepository;
        private readonly IMover _mover;
        private readonly Weapon _weapon;

        public Activator(IGameObjectAgregatorRepository agregatorRepository, IMover mover, Weapon weapon)
        {
            _agregatorRepository = agregatorRepository;
            _mover = mover;
            _weapon = weapon;
        }

        public bool Activate(GameObjectAggregator who, GameObjectAggregator target, double gameTimeSeconds)
        {
            _mover.StopMoving(who);

            var projectile = new GameObjectAggregatorBuilder(ProjectileTypes.Stone)
                .AddArea(who.RootCell, new Coordiante[] { who.RootCell })
                .AddAttribute(AttackAttributes.Damage, _weapon.Damage)
                .AddAttribute(AttackAttributes.DamageType, _weapon.DamageType)
                .AddAttribute(AttackAttributes.TargetPosition, target.RootCell)
                .AddAttribute(MovementAttributes.Speed, 0.5F)
                .AddInteraction<Interaction>()
                .Build();

            _agregatorRepository.Add(projectile);
            _mover.MoveTo(projectile, target.RootCell, null, false);

            who.SetAttributeValue(AttackAttributes.LastTarget, target.GameObject.Id);
            who.SetAttributeValue(AttackAttributes.LastAttackTime, gameTimeSeconds);
            _agregatorRepository.Update(who);
            return true;
        }
    }

    internal class Interaction : ICharacterInteraction
    {
        private readonly ICharacterDamageService _damageService;
        private readonly IGameObjectAgregatorRepository _agregatorRepository;

        public Interaction(ICharacterDamageService damageService, IGameObjectAgregatorRepository agregatorRepository)
        {
            _damageService = damageService;
            _agregatorRepository = agregatorRepository;
        }

        public void Interact(GameObjectAggregator who, GameObjectAggregator with, Coordiante interactionPoint)
        {
            if (with.GameObject.ObjectType == CharacterTypes.Default)
            {
                var damage = who.GetAttributeValue(AttackAttributes.Damage);
                _damageService.Damage(new Character(with), damage);
                _agregatorRepository.Remove(who);
            }
            else if (who.GetAttributeValue(AttackAttributes.TargetPosition) == interactionPoint)
                _agregatorRepository.Remove(who);
        }
    }
}