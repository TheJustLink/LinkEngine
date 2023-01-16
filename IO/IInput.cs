namespace LinkEngine.IO
{
    public interface IInput<out T>
    {
        T Read();
    }
}