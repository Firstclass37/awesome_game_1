namespace My_awesome_character.Core.Game
{
    public struct Coordiante
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