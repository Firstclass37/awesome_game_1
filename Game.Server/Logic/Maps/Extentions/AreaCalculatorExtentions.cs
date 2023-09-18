using Game.Server.Models.Maps;

namespace Game.Server.Logic.Maps.Extentions
{
    internal static class AreaCalculatorExtentions
    {
        public static bool TryGetArea(this IAreaCalculator areaCalculator, Coordiante root, AreaSize areaSize, out Coordiante[] coordiantes)
        {
            coordiantes = Array.Empty<Coordiante>();
            try
            {
                coordiantes = areaCalculator.GetArea(root, areaSize);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}