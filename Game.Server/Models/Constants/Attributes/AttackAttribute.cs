using Game.Server.Models.GameObjects;

namespace Game.Server.Models.Constants.Attributes
{
    internal static class AttackAttributeTypes
    {
        public const string Weapon = "Weapon";
        public const string LastAttackTime = "LastAttackTime";
        public const string LastTarget = "LastTarget";
        public const string Direction = "Direction";
    }

    internal static class AttackAttributes
    {
        public static GameObjectAttribute<double> LastAttackTime => new GameObjectAttribute<double>(AttackAttributeTypes.LastAttackTime);
        public static GameObjectAttribute<Guid?> LastTarget => new GameObjectAttribute<Guid?> (AttackAttributeTypes.LastTarget);
        public static GameObjectAttribute<string> Weapon => new GameObjectAttribute<string>(AttackAttributeTypes.Weapon);
    }
}