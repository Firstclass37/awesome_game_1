using Game.Server.Models.Maps;
using My_awesome_character.Core.Game;

namespace My_awesome_character.Core.Conveters
{
    internal static class CoordinateMapper
    {
        public static CoordianteUI ToUi(this Coordiante coordiante) => new CoordianteUI(coordiante.X, coordiante.Y);
    }
}