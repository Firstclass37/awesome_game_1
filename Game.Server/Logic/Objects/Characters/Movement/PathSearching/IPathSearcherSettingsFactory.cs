using Game.Server.Logic.Objects.Characters.Movement.PathSearching.AStar;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Characters.Movement.PathSearching
{
    internal interface IPathSearcherSettingsFactory
    {
        PathSearcherSetting<Coordiante> Create(INieighborsSearchStrategy<Coordiante> nieighborsSearchStrategy);
    }
}