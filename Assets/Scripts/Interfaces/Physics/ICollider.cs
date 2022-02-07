using System;

namespace Interfaces.Physics
{
    public interface ICollider
    { 
        event Action<ICollision> CollisionExit;
        void CollisionExitEvent(ICollision info);
    }
}