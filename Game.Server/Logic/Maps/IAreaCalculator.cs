using Game.Server.Models.Maps;

namespace Game.Server.Logic.Maps
{
    internal interface IAreaCalculator
    {
        Coordiante[] GetArea(Coordiante root, AreaSize areaSize);
    }
}