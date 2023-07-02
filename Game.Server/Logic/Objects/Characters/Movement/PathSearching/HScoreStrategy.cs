using Game.Server.Logic.Objects.Characters.Movement.PathSearching.AStar;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Characters.Movement.PathSearching
{
    internal class HScoreStrategy : IHScoreStrategy<Coordiante>
    {
        public double Get(Coordiante start, Coordiante end) =>
             Math.Pow(Math.Pow(start.X - end.X, 2) + Math.Pow(start.Y - end.Y, 2), 0.5);
    }
}