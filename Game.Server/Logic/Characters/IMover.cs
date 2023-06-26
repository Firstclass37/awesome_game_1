using Game.Server.Models.Maps;
using Game.Server.Models.Temp;

namespace Game.Server.Logic.Characters
{
    internal interface IMover
    {
        void MoveTo(GameObjectAggregator gameObject, Coordiante coordiante);
        void StopMoving(GameObjectAggregator gameObject);
    }
}