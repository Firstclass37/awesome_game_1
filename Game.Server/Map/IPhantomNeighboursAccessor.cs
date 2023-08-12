using Game.Server.Models.Constants;
using Game.Server.Models.Maps;

namespace Game.Server.Map
{
    public interface IPhantomNeighboursAccessor
    {
        IReadOnlyDictionary<Coordiante, Direction> GetNeightborsOf(Coordiante coordiante);
    }
}