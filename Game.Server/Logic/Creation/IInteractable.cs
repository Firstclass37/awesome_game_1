
using Game.Server.Models.Maps;
using Game.Server.Models.Temp;

namespace Game.Server.Logic.Building
{
    internal interface IInteractionAction
    {
        void Interacte(GameObject gameObject, Coordiante interactionCoordinate);
    }
}