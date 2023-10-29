using Game.Server.Models.Constants;
using Game.Server.Models.Weapons;

namespace Game.Server.Logic.Weapons.Stone
{
    internal class Metadata : IWeaponMetadata
    {
        public Weapon Weapon => new(Name: WeaponsTypes.Stone, Damage: 10, Distance: 10, Speed: 2, DamageType: DamageType.Phisical);

        public IWeaponActivator Activator => throw new NotImplementedException();

        public IActivateCondition ActivateCondition => throw new NotImplementedException();

        public ITargetLocator TargetLocator => throw new NotImplementedException();
    }
}