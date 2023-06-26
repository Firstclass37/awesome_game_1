using Game.Server.Models.Constants;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Characters
{
    internal interface INeighboursAccessor
    {
        Coordiante[] GetNeighboursOf(Coordiante mapCell);

        Dictionary<Coordiante, Direction> GetDirectedNeighboursOf(Coordiante mapCell);
    }
}