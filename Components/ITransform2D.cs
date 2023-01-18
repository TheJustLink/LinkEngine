using System.Numerics;

namespace LinkEngine.Components
{
    public interface ITransform2D : IComponent
    {
        Vector2 Position { get; set; }
        float Rotation { get; set; }
        Vector2 Scale { get; set; }

        Vector2 LocalPosition { get; set; }
        float LocalRotation { get; set; }
        Vector2 LocalScale { get; set; }
    }
}