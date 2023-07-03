using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Buidling
{
    internal interface IAreaGetter
    {
        Coordiante[] GetArea(Coordiante root);
    }
}