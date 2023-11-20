using Game.Server.DataAccess;
using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Weapons.Common;
using Game.Server.Models.Constants;
using Game.Server.Models.Weapons;

namespace Game.Server.Logic.Weapons.Arms
{
    internal class Matadata : IWeaponMetadata
    {
        private readonly IMover _mover;
        private readonly ICharacterDamageService _characterDamageService;
        private readonly IGameObjectAgregatorRepository _gameObjectAgregatorRepository;
        private readonly IAreaCalculator _areaCalculator;
        private readonly IGameObjectAccessor _gameObjectAccessor;

        public Matadata(IMover mover, ICharacterDamageService characterDamageService,
            IGameObjectAgregatorRepository gameObjectAgregatorRepository,
            IAreaCalculator areaCalculator, IGameObjectAccessor gameObjectAccessor)
        {
            _mover = mover;
            _characterDamageService = characterDamageService;
            _gameObjectAgregatorRepository = gameObjectAgregatorRepository;
            _areaCalculator = areaCalculator;
            _gameObjectAccessor = gameObjectAccessor;
        }

        public Weapon Weapon => new(Name: WeaponsTypes.Arms, Damage: 35, DistanceMin: 1,  DistanceMax: 1, Speed: 2, DamageType: DamageType.Phisical);

        public IWeaponActivator Activator => new Activator(_mover, _characterDamageService, _gameObjectAgregatorRepository, Weapon);

        public ITargetLocator TargetLocator => new DistanceTargetLocator(_areaCalculator, _gameObjectAccessor, _gameObjectAgregatorRepository, Weapon);

        public IActivateCondition ActivateCondition => new AttackSpeedCondition(Weapon);
    }
}