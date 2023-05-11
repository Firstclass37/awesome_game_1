using My_awesome_character.Core.Game.Movement.Path;
using System;

namespace My_awesome_character.Core.Game.Movement.Path_1
{
    internal class HScoreStrategy : IHScoreStrategy<MapCell>
    {
        public double Get(MapCell start, MapCell end) => 
             Math.Pow(Math.Pow(start.X - end.X, 2) + Math.Pow(start.Y - end.Y, 2), 0.5);
    }
}