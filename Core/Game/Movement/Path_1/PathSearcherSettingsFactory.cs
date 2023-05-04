using My_awesome_character.Core.Game.Movement.Path;

namespace My_awesome_character.Core.Game.Movement.Path_1
{
    internal class PathSearcherSettingsFactory : IPathSearcherSettingsFactory
    {
        public PathSearcherSetting<MapCell> Create(INieighborsSearchStrategy<MapCell> nieighborsSearchStrategy) =>
            new PathSearcherSetting<MapCell>
            {
                FScoreStrategy = new FScoreStrategy(),
                GScoreStrategy = new GScoreStrategy(),
                NeighborsSearchStrategy = nieighborsSearchStrategy
            };
    }
}