using Game.Server.Logic.Objects._Interactions;

namespace Game.Server.Logic.Objects._Core
{
    internal interface IInteractionsCollection
    {
        ICharacterInteraction Get(string type);
    }
}