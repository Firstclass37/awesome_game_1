using Game.Server.Logic.Characters.Movement.PathSearching.Base;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Characters.Movement.PathSearching
{
    internal class GScoreStrategy : IGScoreStrategy<Coordiante>
    {
        public double Get(Coordiante start, Coordiante end) => 0.2;
    }
}