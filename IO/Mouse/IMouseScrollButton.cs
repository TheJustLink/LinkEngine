namespace LinkEngine.IO.Mouse
{
    public interface IMouseScrollButton : IMouseButton
    {
        float ScrollDelta { get; }
    }
}