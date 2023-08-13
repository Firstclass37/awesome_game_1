using Game.Server.Logic.Objects.Characters.Movement.PathSearching.AStar;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Characters.Movement.PathSearching
{
    internal class HScoreStrategy : IHScoreStrategy<Coordiante>
    {
        public double Get(Coordiante start, Coordiante end) =>
             Math.Pow(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2), 0.5);
    }
}