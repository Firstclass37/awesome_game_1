using Game.Server.Models.Maps;

namespace Game.Server.Logic.Building
{
    public interface IMap
    {
        Coordiante GetActualCell(Coordiante coordiante);
    }
}