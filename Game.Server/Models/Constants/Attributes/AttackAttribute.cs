using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Models.Constants.Attributes
{
    internal static class AttackAttributeTypes
    {
        public const string Weapon = "AttackAttributeTypes.Weapon";
        public const string LastAttackTime = "AttackAttributeTypes.LastAttackTime";
        public const string LastTarget = "AttackAttributeTypes.LastTarget";
        public const string Direction = "AttackAttributeTypes.Direction";
        public const string Damage = "AttackAttributeTypes.Damage";
        public const string DamageType = "AttackAttributeTypes.DamageType";
        public const string TargetPosition = "AttackAttributeTypes.TargetPosition";
    }

    internal static class AttackAttributes
    {
        public static GameObjectAttribute<double> LastAttackTime => new GameObjectAttribute<double>(AttackAttributeTypes.LastAttackTime);
        public static GameObjectAttribute<Guid?> LastTarget => new GameObjectAttribute<Guid?> (AttackAttributeTypes.LastTarget);
        public static GameObjectAttribute<string> Weapon => new GameObjectAttribute<string>(AttackAttributeTypes.Weapon);
        public static GameObjectAttribute<double> Damage => new GameObjectAttribute<double>(AttackAttributeTypes.Damage);
        public static GameObjectAttribute<string> DamageType => new GameObjectAttribute<string>(AttackAttributeTypes.DamageType);
        public static GameObjectAttribute<Coordiante> TargetPosition => new GameObjectAttribute<Coordiante>(AttackAttributeTypes.TargetPosition);
    }
}