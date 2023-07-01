using Game.Server.Models.Constants;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Maps
{
    internal interface IMapGrid
    {
        IReadOnlyDictionary<Coordiante, Direction> GetNeightborsOf(Coordiante coordiante);

        Direction GetDirectionOfNeightbor(Coordiante coordiante, Coordiante neighbor);
    }
}