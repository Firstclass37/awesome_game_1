using System;

namespace My_awesome_character.Core.Game
{
    internal class CommonPeriodicAction : IPeriodicAction
    {
        private Action _action;

        public CommonPeriodicAction(Action action, double period, double currentTime)
        {
            _action = action;
            PeriodSeconds = period;
            LastTriggerTimeSeconds = currentTime;
        }

        public double PeriodSeconds { get; set; }

        public double LastTriggerTimeSeconds { get; set; }

        public void Trigger()
        {
            if (_action != null)
                _action();
        }
    }
}