namespace LinkEngine.Components
{
    public interface IComponent
    {
        bool IsEnabled { get; }

        void Enable();
        void Disable();
    }
}