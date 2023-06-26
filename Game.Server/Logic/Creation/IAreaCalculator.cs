using Game.Server.Models.Maps;

namespace Game.Server.Logic.Building
{
    internal interface IAreaCalculator
    {
        Coordiante[] Get2x2Area(Coordiante root);
    }
}