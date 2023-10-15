using Game.Server.Models.Constants;
using My_awesome_character.Core.Game.Constants;

namespace My_awesome_character.Core.Conveters
{
    internal static class DirectionMapper
    {
        public static DirectionUI ToUi(this Direction direction) => (DirectionUI)((int)direction);
    }
}