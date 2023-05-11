namespace My_awesome_character.Core.Game.Events
{
    public class MouseClickEvent
    {
        public MouseButtonType MouseButtonType { get; set; }

        public double GlobalX { get; set; }

        public double GlobalY { get; set; }
    }

    public enum MouseButtonType
    {
        Left,
        Right
    }
}