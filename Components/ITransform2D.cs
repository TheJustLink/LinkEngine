using LinkEngine.Math;

namespace LinkEngine.Components
{
    public interface ITransform2D : IComponent
    {
        bool HasParent { get; }
        ITransform2D? Parent { get; set; }

        Vector2 Position { get; set; }
        float RotationInRadians { get; set; }
        float RotationInDegrees { get; set; }
        Vector2 Scale { get; set; }

        Vector2 LocalPosition { get; set; }
        float LocalRotationInRadians { get; set; }
        float LocalRotationInDegrees { get; set; }
        Vector2 LocalScale { get; set; }
    }
}