using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects.Characters.Movement.PathSearching.AStar;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Characters.Movement.PathSearching
{
    internal class HScoreStrategy : IHScoreStrategy<Coordiante>
    {
        private readonly IMatrixGrid _matrixGrid;

        public HScoreStrategy(IMatrixGrid matrixGrid)
        {
            _matrixGrid = matrixGrid;
        }

        public double Get(Coordiante start, Coordiante end)
        {
            var startIndex = _matrixGrid.GetCoordinateFor(start);
            var endIndex = _matrixGrid.GetCoordinateFor(end);

            return Math.Pow(Math.Pow(endIndex.X - startIndex.X, 2) + Math.Pow(endIndex.Y - startIndex.Y, 2), 0.5);
        }
    }
}