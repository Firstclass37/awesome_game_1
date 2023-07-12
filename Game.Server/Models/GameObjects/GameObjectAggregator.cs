namespace Game.Server.Models.GameObjects
{
    internal class GameObjectAggregator
    {
        public GameObjectAggregator()
        {
            Attributes = new List<GameObjectToAttribute>(0);
            Area = new List<GameObjectPosition>(0);
            PeriodicActions = new List<PeriodicAction>(0);
            Interactions = new List<GameObjectInteraction>(0);
        }

        public GameObject GameObject { get; set; }

        public List<GameObjectToAttribute> Attributes { get; set; }

        public List<GameObjectPosition> Area { get; set; }

        public List<PeriodicAction> PeriodicActions { get; set; }

        public List<GameObjectInteraction> Interactions { get; set; }


        internal T GetAttributeValue<T>(string attributeType) => (T)Attributes.First(a => a.AttributeType == attributeType).Value;
    }
}