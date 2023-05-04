using My_awesome_character.Core.Game.Movement.Path;

namespace My_awesome_character.Core.Game.Movement.Path_1
{
    internal interface IPathSearcherSettingsFactory
    {
        PathSearcherSetting<MapCell> Create(INieighborsSearchStrategy<MapCell> nieighborsSearchStrategy);
    }
}