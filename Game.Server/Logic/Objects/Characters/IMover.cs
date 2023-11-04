using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Characters
{
    internal interface IMover
    {
        void MoveTo(GameObjectAggregator gameObject, Coordiante coordiante, Guid? initiator = null, bool? onlyRoadPath = true);

        void StopMoving(GameObjectAggregator gameObject);
    }
}