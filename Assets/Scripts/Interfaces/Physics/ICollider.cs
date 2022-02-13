using System;

namespace Interfaces.Physics
{
    public interface ICollider
    {
        abstract event Action<ICollision> CollisionEnter;
        abstract event Action<ICollision> CollisionExit;
        
        abstract void CollisionEnterEvent(ICollision info);
        abstract void CollisionExitEvent(ICollision info);
    }
}