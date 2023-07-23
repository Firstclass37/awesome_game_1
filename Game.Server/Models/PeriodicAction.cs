namespace Game.Server.Models
{
    internal class PeriodicAction: IEntityObject
    {
        public PeriodicAction(Guid gameObjectId, string actionType, double periodSeconds, double lastTriggerSeconds)
        {
            Id = Guid.NewGuid();
            GameObjectId = gameObjectId;
            ActionType = actionType;
            LastTriggerTimeSeconds = lastTriggerSeconds;
            PeriodSeconds = periodSeconds;
        }

        public Guid Id { get; }

        public Guid GameObjectId { get; }

        public string ActionType { get; }

        public double PeriodSeconds { get; }

        public double LastTriggerTimeSeconds { get; set; }
    }
}