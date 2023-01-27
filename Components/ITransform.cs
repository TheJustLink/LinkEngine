using LinkEngine.Math;

namespace LinkEngine.Components
{
    public interface ITransform : ITransform2D
    {
        new ITransform? Parent { get; set; }

        new Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }
        new Vector3 Scale { get; set; }

        new Vector3 LocalPosition { get; set; }
        Quaternion LocalRotation { get; set; }
        new Vector3 LocalScale { get; set; }
    }
}