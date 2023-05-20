namespace My_awesome_character.Core.Game
{
    public interface IPeriodicAction
    {
        public double PeriodSeconds { get; set; }

        public double LastTriggerTimeSeconds { get; set; }

        void Trigger();
    }
}