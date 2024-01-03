using Game.Server.Models.Constants;

namespace Game.Server.Models.Weather
{
    internal record GameTime : IEntityObject
    {
        public GameTime()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public int Hours { get; init; }

        public TimeOfDay TimeOfDay { get; init; }
    }
}