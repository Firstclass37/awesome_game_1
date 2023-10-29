namespace Game.Server.Logic.Weapons
{
    internal interface IArsenal
    {
        IWeaponMetadata Get(string name);
    }

    internal class Arsenal : IArsenal
    {
        private readonly IWeaponMetadata[] _weaponMetadatas;

        public Arsenal(IWeaponMetadata[] weaponMetadatas)
        {
            _weaponMetadatas = weaponMetadatas;
        }

        public IWeaponMetadata Get(string name)
        {
            var found = _weaponMetadatas.FirstOrDefault(w => w.Weapon.Name == name);
            if (found == null)
                throw new ArgumentOutOfRangeException($"the weapon [{name}] was not found");

            return found;
        }
    }
}