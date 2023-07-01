using Game.Server.Models.Maps;

namespace Game.Server.Logic.Maps
{
    internal interface IAreaCalculator
    {
        Coordiante[] Get2x2Area(Coordiante root);

        Coordiante[] Get3X3Area(Coordiante root);
    }
}