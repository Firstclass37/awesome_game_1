namespace Game.Server.Models.Maps
{
    public record Coordiante
    {
        public Coordiante(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }
    }
}