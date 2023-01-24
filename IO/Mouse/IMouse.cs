using System.Numerics;

namespace LinkEngine.IO.Mouse
{
    public interface IMouse
    {
        bool Enabled { get; }
        Vector2 PositionInScreen { get; }
        Vector2 PositionInWorld { get; }

        IMouseButton LeftButton { get; }
        IMouseButton RightButton { get; }

        IMouseScrollButton ScrollButton { get; }

        IMouseButton[] AdditionalButtons { get; }
        IMouseButton[] AllButtons { get; }
    }
}