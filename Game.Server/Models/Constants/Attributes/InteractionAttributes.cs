using Game.Server.Models.GameObjects;
using Game.Server.Models.Maps;

namespace Game.Server.Models.Constants.Attributes
{
    internal class InteractionAttributesTypes
    {
        public const string Interactable = "Interaction.Interactable";
        public const string InteractionArea = "Interaction.Area";
        public const string LastInteractionTime = "Interaction.LastInteractionTime";
    }

    internal static class InteractionAttributes
    {
        public static GameObjectAttribute<Coordiante[]> InteractionArea => new GameObjectAttribute<Coordiante[]>(InteractionAttributesTypes.InteractionArea);
        public static GameObjectAttribute<bool> Interactable => new GameObjectAttribute<bool>(InteractionAttributesTypes.Interactable);
        public static GameObjectAttribute<double> LastInteractionTime => new GameObjectAttribute<double>(InteractionAttributesTypes.LastInteractionTime);
    }
}