using System.Numerics;

namespace LinkEngine.Components
{
    public interface ITransform : ITransform2D
    {
        bool HasParent { get; }
        ITransform Parent { get; set; }

        new Vector3 Position { get; set; }
        new Quaternion Rotation { get; set; }
        new Vector3 Scale { get; set; }

        new Vector3 LocalPosition { get; set; }
        new Quaternion LocalRotation { get; set; }
        new Vector3 LocalScale { get; set; }
    }

    public interface ITransform2D
    {
        Vector2 Position { get; set; }
        float Rotation { get; set; }
        Vector2 Scale { get; set; }

        Vector2 LocalPosition { get; set; }
        float LocalRotation { get; set; }
        Vector2 LocalScale { get; set; }
    }
}