using Game.Server.Models.Constants;
using Game.Server.Models.GameObjects;
using Game.Server.Models.GamesObjectList;
using Game.Server.Models.Maps;

namespace Game.Server.Logic.Objects._Interactions
{
    internal abstract class CharacterInteraction : ICharacterInteraction
    {
        protected abstract void OnInteract(GameObjectAggregator who, Character with, Coordiante interactionPoint);

        public void Interact(GameObjectAggregator who, GameObjectAggregator with, Coordiante interactionPoint)
        {
            if (with.GameObject.ObjectType == CharacterTypes.Default)
                OnInteract(who, new Character(with), interactionPoint);
        }
    }
}