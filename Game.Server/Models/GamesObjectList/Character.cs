using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Models.GamesObjectList
{
    internal class Character
    {
        public Guid Id { get; set; }

        public GameObjectAggregator GameObject { get; }

        public double SpeedMultiplier { get; set; }

        public Coordiante Position { get; set; }
    }


    internal class CharacterMovement: IEntityObject
    {
        public CharacterMovement()
        {
            Id = Guid.NewGuid();
            Active = true;
        }

        public Guid Id { get; }

        public Guid CharacterId { get; set; }

        public Guid? MovementIniciator { get; set; }

        public Coordiante[] Path { get; set; }

        public double LastMovement { get; set; }

        public bool Active { get; set; }
    }
}