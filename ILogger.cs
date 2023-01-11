namespace GameProject
{
    public interface ILogger : IOutput<string>
    {
        void WriteWarning(string message);
        void WriteError(string message);
    }
}