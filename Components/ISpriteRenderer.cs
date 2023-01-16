using LinkEngine.Assets;
using LinkEngine.IO;

namespace LinkEngine.Components
{
    public interface ISpriteRenderer : IOutput<ISprite>
    {
        ISprite Sprite { get; set; }
    }
}