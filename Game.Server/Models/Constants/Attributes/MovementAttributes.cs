﻿using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Models.Constants.Attributes
{
    internal static class MovementAttributesTypes
    {
        public const string Speed = "MovementAttributesTypes.Speed";
        public const string LastMovementTime = "MovementAttributesTypes.LastMovementTime";
        public const string MovementPath = "MovementAttributesTypes.MovementPath";
        public const string MovingTo = "MovementAttributesTypes.MovingTo";
        public const string Iniciator = "MovementAttributesTypes.Iniciator";
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