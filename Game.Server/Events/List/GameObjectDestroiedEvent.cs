namespace Game.Server.Events.List
{
    public class GameObjectDestroiedEvent
    {
        public Guid ObjectId { get; set; }

        public string ObjectType { get; set; }
    }
}