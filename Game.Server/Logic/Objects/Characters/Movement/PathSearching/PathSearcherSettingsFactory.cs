using Game.Server.Logic.Maps;
using Game.Server.Logic.Objects.Characters.Movement.PathSearching.AStar;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Characters.Movement.PathSearching
{
    internal class PathSearcherSettingsFactory : IPathSearcherSettingsFactory
    {
        private readonly IMatrixGrid _matrixGrid;

        public PathSearcherSettingsFactory(IMatrixGrid matrixGrid)
        {
            _matrixGrid = matrixGrid;
        }

        public PathSearcherSetting<Coordiante> Create(INieighborsSearchStrategy<Coordiante> nieighborsSearchStrategy) =>
            new PathSearcherSetting<Coordiante>
            {
                HScoreStrategy = new HScoreStrategy(_matrixGrid),
                GScoreStrategy = new GScoreStrategy(),
                NeighborsSearchStrategy = nieighborsSearchStrategy
            };
    }
}