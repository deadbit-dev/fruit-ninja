using System;

namespace Interfaces.Physics
{
    public interface ICollider
    {
        event Action<ICollision> CollisionEnter;
        event Action<ICollision> CollisionExit;

        void CollisionEnterEvent(ICollision info);
        void CollisionExitEvent(ICollision info);
    }
}