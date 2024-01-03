namespace Game.Server.Models
{
    internal record Game : IEntityObject
    {
        public Game()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public double GameTimeSeconds { get; set; }

        public int[] Players { get; set; }
    }
}