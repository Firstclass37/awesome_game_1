using Game.Server.Models.GameObjects;

namespace Game.Server.Models.Constants.Attributes
{
    internal static class AttackAttributeTypes
    {
        public const string Distance = "Distance";
        public const string Direction = "Direction";
        public const string LastAttackTime = "LastAttackTime";
        public const string AttackCooldown = "AttackCooldown";
    }

    internal static class AttackAttributes
    {
        public static GameObjectAttribute<int> Distance => new GameObjectAttribute<int>(AttackAttributeTypes.Distance);
        public static GameObjectAttribute<double> LastAttackTime => new GameObjectAttribute<double>(AttackAttributeTypes.LastAttackTime);
        public static GameObjectAttribute<double> Speed => new GameObjectAttribute<double>(AttackAttributeTypes.AttackCooldown);
    }
}