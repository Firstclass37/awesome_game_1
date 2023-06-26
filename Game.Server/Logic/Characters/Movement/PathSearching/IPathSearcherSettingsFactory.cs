using Game.Server.Logic.Characters.Movement.PathSearching.Base;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Characters.Movement.PathSearching
{
    internal interface IPathSearcherSettingsFactory
    {
        PathSearcherSetting<Coordiante> Create(INieighborsSearchStrategy<Coordiante> nieighborsSearchStrategy);
    }
}