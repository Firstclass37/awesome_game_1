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

        public double Speed => GameObject.GetAttributeValue<double>(CharacterAttributes.Speed);

        public Coordiante Position => GameObject.Area.First().Coordiante;

        public Coordiante[] DamageArea => GameObject.GetAttributeValue<Coordiante[]>(CharacterAttributes.DamageArea);
    }
}