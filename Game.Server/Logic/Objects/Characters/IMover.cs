using Game.Server.Models.GamesObjectList;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects.Characters
{
    internal interface IMover
    {
        Models.Movement GetCurrentMovement(Character character);

        void MoveTo(Character gameObject, Coordiante coordiante, Guid? initiator = null);

        void StopMoving(Character gameObject);
    }
}