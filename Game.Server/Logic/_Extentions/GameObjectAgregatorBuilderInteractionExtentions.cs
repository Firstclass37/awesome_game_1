using Game.Server.DataBuilding;
using Game.Server.Logic.Objects._Interactions;
using Game.Server.Models.Constants;

namespace Game.Server.Logic._Extentions
{
    internal static class GameObjectAgregatorBuilderInteractionExtentions
    {
        public static GameObjectAggregatorBuilder AsInteractable<TInteraction>(this GameObjectAggregatorBuilder gameObjectAggregatorBuilder)
            where TInteraction: ICharacterInteraction
        {
            gameObjectAggregatorBuilder.AddAttribute(AttrituteTypes.Interactable);
            gameObjectAggregatorBuilder.AddInteraction<TInteraction>();

            return gameObjectAggregatorBuilder;
        }
    }
}
