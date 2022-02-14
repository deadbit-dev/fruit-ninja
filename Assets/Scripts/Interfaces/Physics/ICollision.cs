using UnityEngine;

namespace Interfaces.Physics
{
    public interface ICollision
    {
        BaseCollider Collider { get; set; }
        Vector3 ContactPosition { get; set; }
    }
}