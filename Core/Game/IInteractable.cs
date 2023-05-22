using System;

namespace My_awesome_character.Core.Game
{
    public interface IInteractable
    {
        IInteractionAction InteractionAction { get; }
    }

    public interface IInteractionAction
    {
        void Interacte(character character);
    }

    public class CommonInteractionAction : IInteractionAction
    {
        private readonly Action<character> _action;

        public CommonInteractionAction(Action<character> action)
        {
            _action = action;
        }

        public void Interacte(character character)
        {
            _action(character);
        }
    }
}