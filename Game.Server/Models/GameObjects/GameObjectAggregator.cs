namespace Game.Server.Models.GameObjects
{
    internal class GameObjectAggregator
    {
        public GameObject GameObject { get; set; }

        public IReadOnlyCollection<GameObjectToAttribute> Attributes { get; set; }

        public IReadOnlyCollection<GameObjectPosition> Area { get; set; }

        public IReadOnlyCollection<PeriodicAction> PeriodicActions { get; set; }

        public IReadOnlyCollection<GameObjectInteraction> Interactions { get; set; }


        internal T GetAttributeValue<T>(string attributeType) => (T)Attributes.First(a => a.AttributeType == attributeType).Value;
    }
}