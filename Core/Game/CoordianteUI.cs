namespace My_awesome_character.Core.Game
{
    public record CoordianteUI
    {
        public CoordianteUI(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; } 

        public int Y { get; }
    }
}