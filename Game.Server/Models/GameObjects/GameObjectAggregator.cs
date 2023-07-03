namespace Game.Server.Models.GameObjects
{
    internal class GameObjectAggregator
    {
        public GameObject GameObject { get; set; }

        public List<GameObjectToAttribute> Attributes { get; set; }

        public List<GameObjectPosition> Area { get; set; }

        public List<PeriodicAction> PeriodicActions { get; set; }

        public List<GameObjectInteraction> Interactions { get; set; }


        internal T GetAttributeValue<T>(string attributeType) => (T)Attributes.First(a => a.AttributeType == attributeType).Value;
    }
}