using Game.Server.Models.GameObjects;

namespace Game.Server.Models.Constants.Attributes
{
    internal static class HealthAttributeTypes
    {
        public static string Health => nameof(Health);
    }

    internal static class HealthAttributes
    {
        public static GameObjectAttribute<double> Health => new GameObjectAttribute<double>(HealthAttributeTypes.Health);
    }
}