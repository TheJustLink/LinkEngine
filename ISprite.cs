namespace GameProject
{
    public interface ISprite
    {
        bool Packed { get; }
        
        // Composition (ISprite : IContent) or inheritance?
        // IContentLookup with T Lookup<T>() where T : IContent?
    }
}