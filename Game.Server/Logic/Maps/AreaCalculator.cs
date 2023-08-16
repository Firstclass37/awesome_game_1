using Game.Server.Map;
using Game.Server.Models.Constants;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Maps
{
    internal record AreaSize
    {
        public static AreaSize Area2x2 => new AreaSize(2, 2);

        public static AreaSize Area1x1 => new AreaSize(1, 1);

        public AreaSize(int height, int width)
        {
            Height = height > 0 ? height : throw new ArgumentException("height must be above zero");
            Width = width > 0 ? width : throw new ArgumentException("width must be above zero");
        }

        public int Height { get; }

        public int Width { get; }
    }

    internal class AreaCalculator : IAreaCalculator
    {
        private readonly IMapGrid _mapGrid;

        public AreaCalculator(IMapGrid mapGrid)
        {
            _mapGrid = mapGrid;
        }

        public Coordiante[] GetArea(Coordiante root, AreaSize areaSize) 
        { 
            ArgumentNullException.ThrowIfNull(areaSize);

            var container = new List<Coordiante>();

            var points = MoveTo(root, Direction.Bottom, areaSize.Height - 1).Union(new[] { root }).ToArray();
            foreach(var point in points)
            {
                var row = MoveTo(point, Direction.Right, areaSize.Width - 1);
                container.Add(point);
                container.AddRange(row);
            }

            return container.ToArray();
        }

        private IEnumerable<Coordiante> MoveTo(Coordiante coordiante, Direction direction, int count)
        {
            var current = coordiante;
            var i = count;
            while (i-- > 0)
            {
                current = _mapGrid.GetNeightborsOf(current).First(v => v.Value == direction).Key;
                yield return current;
            }
        }
    }
}