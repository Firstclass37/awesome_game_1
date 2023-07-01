using Game.Server.Models.GamesObjectList;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Characters
{
    internal interface IMover
    {
        CharacterMovement GetCurrentMovement(Character character);
        void MoveTo(Character gameObject, Coordiante coordiante, Guid? initiator = null);
        void StopMoving(Character gameObject);
    }
}