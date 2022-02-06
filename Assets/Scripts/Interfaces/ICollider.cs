using System;
using Components.Physics;
namespace Interfaces
{
    public interface ICollider
    { 
        event Action<Collision> CollisionExit;
        void CollisionExitEvent(Collision info);
    }
}