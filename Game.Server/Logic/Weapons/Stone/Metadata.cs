using Game.Server.DataAccess;
using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects.Characters;
using Game.Server.Logic.Weapons.Common;
using Game.Server.Models.Constants;
using Game.Server.Models.Weapons;

namespace Game.Server.Logic.Weapons.Stone
{
    internal class Metadata : IWeaponMetadata
    {
        private readonly IMover _mover;
        private readonly IGameObjectAgregatorRepository _gameObjectAgregatorRepository;
        private readonly IAreaCalculator _areaCalculator;
        private readonly IGameObjectAccessor _gameObjectAccessor;
        private readonly IGameObjectAgregatorRepository _agregatorRepository;

        public Metadata(IMover mover, IGameObjectAgregatorRepository gameObjectAgregatorRepository,
            IAreaCalculator areaCalculator, IGameObjectAccessor gameObjectAccessor,
            IGameObjectAgregatorRepository agregatorRepository)
        {
            _mover = mover;
            _gameObjectAgregatorRepository = gameObjectAgregatorRepository;
            _areaCalculator = areaCalculator;
            _gameObjectAccessor = gameObjectAccessor;
            _agregatorRepository = agregatorRepository;
        }

        public Weapon Weapon => new(Name: WeaponsTypes.Stone, Damage: 10, Distance: 5, Speed: 3, DamageType: DamageType.Phisical);

        public IWeaponActivator Activator => new Activator(_agregatorRepository, _mover, Weapon);

        public IActivateCondition ActivateCondition => new AttackSpeedCondition(Weapon);

        public ITargetLocator TargetLocator => new DistanceTargetLocator(_areaCalculator, _gameObjectAccessor, _gameObjectAgregatorRepository, Weapon);
    }
}