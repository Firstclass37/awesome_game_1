using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Models.Constants.Attributes
{
    internal static class MovementAttributesTypes
    {
        public const string Speed = "Speed";
        public const string LastMovementTime = "LastMovementTime";
        public const string MovementPath = "MovementPath";
        public const string MovingTo = "MovingTo";
        public const string Iniciator = "Iniciator";
    }

    internal static class MovementAttributes
    {
        public static GameObjectAttribute<double> Speed => new GameObjectAttribute<double>(MovementAttributesTypes.Speed);

        public static GameObjectAttribute<double> LastMovementTime => new GameObjectAttribute<double>(MovementAttributesTypes.LastMovementTime);

        public static GameObjectAttribute<Coordiante[]> Movementpath => new GameObjectAttribute<Coordiante[]>(MovementAttributesTypes.MovementPath);

        public static GameObjectAttribute<Coordiante?> MovingTo => new GameObjectAttribute<Coordiante?>(MovementAttributesTypes.MovingTo);

        public static GameObjectAttribute<Guid?> Iniciator => new GameObjectAttribute<Guid?>(MovementAttributesTypes.Iniciator);
    }
}