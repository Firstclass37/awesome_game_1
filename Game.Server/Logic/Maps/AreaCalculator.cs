using Game.Server.Models.Maps;

namespace Game.Server.Logic.Maps
{
    internal class AreaCalculator : IAreaCalculator
    {
        private readonly IMapGrid _mapGrid;

        public AreaCalculator(IMapGrid mapGrid)
        {
            _mapGrid = mapGrid;
        }

        public Coordiante[] Get2x2Area(Coordiante root)
        {
            var neigbors = _mapGrid.GetNeightborsOf(root);
            var rightNeighbor = neigbors.First(n => n.Value == Models.Constants.Direction.Right).Key;

            return new Coordiante[]
            {
                root,
                rightNeighbor,
                neigbors.First(n => n.Value == Models.Constants.Direction.Bottom).Key,
                _mapGrid.GetNeightborsOf(rightNeighbor).First(n => n.Value == Models.Constants.Direction.Bottom).Key
            };
        }

        public Coordiante[] Get3X3Area(Coordiante root)
        {
            throw new NotImplementedException();
        }
    }
}
