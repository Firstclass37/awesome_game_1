using Game.Server.DataAccess;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Models.Constants.Attributes;
using Game.Server.Models.GameObjects;
using Game.Server.Models.GamesObjectList;
using Game.Server.Models.Weapons;

namespace Game.Server.Logic.Weapons.Arms
{
    internal class Activator : IWeaponActivator
    {
        private readonly IMover _mover;
        private readonly ICharacterDamageService _characterDamageService;
        private readonly IGameObjectAgregatorRepository _gameObjectAgregatorRepository;
        private readonly Weapon _weapon;

        public Activator(IMover mover, ICharacterDamageService characterDamageService,
            IGameObjectAgregatorRepository gameObjectAgregatorRepository, Weapon weapon)
        {
            _mover = mover;
            _characterDamageService = characterDamageService;
            _gameObjectAgregatorRepository = gameObjectAgregatorRepository;
            _weapon = weapon;
        }

        public bool Activate(GameObjectAggregator who, GameObjectAggregator target, double gameTimeSeconds)
        {
            _mover.StopMoving(who);
            _characterDamageService.Damage(new Character(target), _weapon.Damage);
            who.SetAttributeValue(AttackAttributes.LastAttackTime, gameTimeSeconds);
            who.SetAttributeValue(AttackAttributes.LastTarget, target.GameObject.Id);
            _gameObjectAgregatorRepository.Update(who);
            return true;
        }
    }
}