using System;
using Components;

namespace Interfaces
{
    public interface ICollider
    { 
        event Action<Collision> CollisionExit;
        void CollisionExitEvent(Collision info);
    }
}