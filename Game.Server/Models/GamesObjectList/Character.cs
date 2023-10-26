using Game.Server.Models.GameObjects;

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
    }

    internal enum CharacterState
    {
        Free,
        Attack,
        Mining,
        Waiting
    }

    internal static class CharacterAttributesTypes
    {
        public const string Speed = "Speed";
        public const string CharacterState = nameof(CharacterState);
    }

    internal static class CharacterAttributes
    {
        public static GameObjectAttribute<double> Speed => new GameObjectAttribute<double>(CharacterAttributesTypes.Speed);
        
        public static GameObjectAttribute<CharacterState> CharacterState => new GameObjectAttribute<CharacterState>(CharacterAttributesTypes.CharacterState);
    }
}