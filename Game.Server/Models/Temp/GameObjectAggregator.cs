namespace Game.Server.Models.Temp
{
    internal class GameObjectAggregator
    {
        public GameObject GameObject { get; set; }

        public IReadOnlyCollection<GameObjectToAttribute> Attributes { get; set; }

        public IReadOnlyCollection<GameObjectPosition> Area { get; set; }
    }
}