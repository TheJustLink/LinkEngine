namespace GameProject
{
    public interface IInput<out T>
    {
        T Read();
    }
}