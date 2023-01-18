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
}