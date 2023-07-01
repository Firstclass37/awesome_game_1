using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Requirements
{
    internal interface ICreationRequirement
    {
        bool Satisfy(Coordiante[] area);
    }
}