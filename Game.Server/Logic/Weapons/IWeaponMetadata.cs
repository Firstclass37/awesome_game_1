using Game.Server.Models.Weapons;

namespace Game.Server.Logic.Weapons
{
    internal interface IWeaponMetadata
    {
        public Weapon Weapon { get; }

        public IWeaponActivator Activator { get; }

        public ITargetLocator TargetLocator { get; }

        public IActivateCondition ActivateCondition { get; }
    }
}