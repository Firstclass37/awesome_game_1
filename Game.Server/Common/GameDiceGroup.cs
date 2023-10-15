namespace Game.Server.Common
{
    internal class GameDiceGroup
    {
        public static GameDiceGroup CreateCube(int count) => new GameDiceGroup(count, 1, 6);

        private readonly int _count;
        private readonly int _minValue = 1;
        private readonly int _maxValue = 6;

        private GameDiceGroup(int count, int minValue, int maxValue)
        {
            this._count = count > 1 ? count : throw new ArgumentException($"count must be more than 1 (was {count})");
            _minValue = minValue;
            _maxValue = maxValue;
        }

        public int Roll() 
        {
            return Enumerable
                .Range(0, _count)
                .Select(i => new Random().Next(_minValue, _maxValue + 1))
                .Sum();
        }
    }
}