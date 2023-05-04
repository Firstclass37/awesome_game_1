using My_awesome_character.Core.Game.Movement.Path;

namespace My_awesome_character.Core.Game.Movement.Path_1
{
    internal class GScoreStrategy : IGScoreStrategy<MapCell>
    {
        public double Get(MapCell start, MapCell end) => 1;
    }
}