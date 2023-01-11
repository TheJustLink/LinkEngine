namespace GameProject
{
    public interface IOutput<in T>
    {
        void Write(T value);
    }
}