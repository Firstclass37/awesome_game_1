using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Interactions
{
    internal interface ICharacterInteraction
    {
        void Interact(GameObjectAggregator who, GameObjectAggregator with, Coordiante interactionPoint);
    }
}