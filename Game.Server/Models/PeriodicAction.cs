namespace Game.Server.Models
{
    internal class PeriodicAction: IEntityObject
    {
        public PeriodicAction(Guid gameObjectId, string actionType, double periodMs, double lastTriggerTimeMs)
        {
            GameObjectId = gameObjectId;
            ActionType = actionType;
            LastTriggerTimeMs = lastTriggerTimeMs;
            PeriodMs = periodMs;
        }

        public Guid Id { get; }

        public Guid GameObjectId { get; }

        public string ActionType { get; }

        public double PeriodMs { get; }

        public double LastTriggerTimeMs { get; set; }
    }
}