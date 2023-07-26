using Game.Server.Logic.Objects._Interactions;

namespace Game.Server.Logic.Objects._Core
{
    internal class InteractionsCollection : IInteractionsCollection
    {
        private readonly Dictionary<string, ICharacterInteraction> _interactions;

        public InteractionsCollection(ICharacterInteraction[] characterInteractions)
        {
            _interactions = characterInteractions
                .GroupBy(i => i.GetType().FullName)
                .ToDictionary(i => i.Key, i => i.First());
        }

        public ICharacterInteraction Get(string type)
        {
            return _interactions.ContainsKey(type)
                ? _interactions[type]
                : throw new ArgumentException($"interaction with type {type} was not found");
        }
    }
}