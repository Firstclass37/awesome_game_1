using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Buidling
{
    internal record CreationParams(string ObjectType, Coordiante Point, int Player, object Args = null);

    internal interface IGameObjectCreator
    {
        GameObjectAggregator Create(CreationParams creationParamsl);

        bool CanCreate(CreationParams creationParams);
    }
}