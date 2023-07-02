using Game.Server.Logic.Objects.Characters.Movement.PathSearching.AStar;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Characters.Movement.PathSearching
{
    internal class GScoreStrategy : IGScoreStrategy<Coordiante>
    {
        public double Get(Coordiante start, Coordiante end) => 0.2;
    }
}