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

        public double SpeedMultiplier { get; set; }

        public Coordiante Position => GameObject.Area.First().Coordiante;
    }
}