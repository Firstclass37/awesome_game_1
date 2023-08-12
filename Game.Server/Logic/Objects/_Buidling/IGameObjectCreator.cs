using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Buidling
{
    internal interface IGameObjectCreator
    {
        GameObjectAggregator Create(string objectType, Coordiante point, object args = null);

        bool CanCreate(string objectType, Coordiante point, object args = null);
    }
}