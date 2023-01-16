namespace LinkEngine.IO
{
    public interface IOutput<in T>
    {
        void Write(T value);
    }
}