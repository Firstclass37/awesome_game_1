using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Models.GamesObjectList
{
    internal class Character
    {
        public Character(GameObjectAggregator gameObjectAggregator) 
        {
            GameObject = gameObjectAggregator ?? throw new ArgumentNullException(nameof(gameObjectAggregator));
        }

        public Guid Id => GameObject.GameObject.Id;

        public GameObjectAggregator GameObject { get; }

        public Coordiante Position => GameObject.Area.First().Coordiante;
    }

    internal static class CharacterAttributesTypes
    {
        public const string DamageArea = "DamageArea";
        public const string Speed = "Speed";
        public const string LastAttackTime = nameof(LastAttackTime);
        public const string AttackCooldown = nameof(AttackCooldown);
    }

    internal static class CharacterAttributes
    {
        public static GameObjectAttribute<double> Speed => new GameObjectAttribute<double>(CharacterAttributesTypes.Speed);
        public static GameObjectAttribute<int> AttackDistance => new GameObjectAttribute<int>(CharacterAttributesTypes.DamageArea);
        public static GameObjectAttribute<double> LastAttackTime => new GameObjectAttribute<double>(CharacterAttributesTypes.LastAttackTime);
        public static GameObjectAttribute<double> AttackSpeed => new GameObjectAttribute<double>(CharacterAttributesTypes.AttackCooldown);

    }
}