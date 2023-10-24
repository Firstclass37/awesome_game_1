using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Models.GamesObjectList
{
    internal class HomeAttributesTypes
    {
        public const string SpawnCell = nameof(SpawnCell);

    }

    internal static class HomeAttributes
    {
        public static GameObjectAttribute<Coordiante> SpawnCell => new GameObjectAttribute<Coordiante>(HomeAttributesTypes.SpawnCell);
    }
}