namespace GameProject
{
    public interface ISpriteRenderer : IOutput<ISprite>
    {
        ISprite Sprite { get; set; }
    }
}