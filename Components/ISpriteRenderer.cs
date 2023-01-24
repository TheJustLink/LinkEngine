using LinkEngine.Assets;
using LinkEngine.Graphics;

namespace LinkEngine.Components
{
    public interface ISpriteRenderer : IComponent
    {
        ISprite? Sprite { get; set; }
        Color Color { get; set; }
    }
}