using Godot;

namespace My_awesome_character.Helpers
{
    internal static class InputEventExtention
    {
        public static bool IsLeftClick(this InputEvent inputEvent)
        {
            if (inputEvent is InputEventMouseButton mouseButtonEvent)
            {
                return mouseButtonEvent.Pressed && mouseButtonEvent.ButtonIndex == MouseButton.Left;
            }
            return false;
        }

        public static bool IsRighClick(this InputEvent inputEvent)
        {
            if (inputEvent is InputEventMouseButton mouseButtonEvent)
            {
                return mouseButtonEvent.Pressed && mouseButtonEvent.ButtonIndex == MouseButton.Right;
            }
            return false;
        }
    }
}