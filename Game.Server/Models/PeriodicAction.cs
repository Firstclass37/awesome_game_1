namespace Game.Server.Models
{
    internal class PeriodicAction: IEntityObject
    {
        public PeriodicAction(Guid gameObjectId, string actionType, double periodSeconds, double lastTriggerTimeSeconds)
        {
            GameObjectId = gameObjectId;
            ActionType = actionType;
            LastTriggerTimeSeconds = lastTriggerTimeSeconds;
            PeriodSeconds = periodSeconds;
        }

        public Guid Id { get; }

        public Guid GameObjectId { get; }

        public string ActionType { get; }

        public double PeriodSeconds { get; }

        public double LastTriggerTimeSeconds { get; }
    }
}