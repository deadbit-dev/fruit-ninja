using System;
using Components.Physics;
namespace Interfaces
{
    public interface ICollider
    { 
        event Action<CollisionInfo> CollisionExit;
        void CollisionExitEvent(CollisionInfo info);
    }
}