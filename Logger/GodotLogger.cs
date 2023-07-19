using Game.Server.Logger;

namespace My_awesome_character.Logger
{
    internal class GodotLogger : ILogger
    {
        public void Info(string message)
        {
            Godot.GD.Print($"L: {message}");
        }
    }
}
