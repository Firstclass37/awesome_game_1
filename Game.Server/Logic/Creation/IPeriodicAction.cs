namespace Game.Server.Logic.Building
{
    public interface IPeriodicAction
    {
        public double PeriodSeconds { get; set; }

        public double LastTriggerTimeSeconds { get; set; }

        void Trigger();
    }
}