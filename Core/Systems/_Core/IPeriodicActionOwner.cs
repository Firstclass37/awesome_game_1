using My_awesome_character.Core.Game;

namespace My_awesome_character.Core.Systems._Core
{
    internal interface IPeriodicActionOwner
    {
        IPeriodicAction PeriodicAction { get; }
    }
}