using Game.Server.Models.GameObjects;
using Game.Server.Models.GamesObjectList;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Interactions
{
    internal interface ICharacterInteraction
    {
        void Interact(GameObjectAggregator gameObject, Character character, Coordiante interactionPoint);
    }
}