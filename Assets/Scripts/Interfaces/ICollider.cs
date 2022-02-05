using Components;

namespace Interfaces
{
    public interface ICollider
    {
        void CollisionEnter(CollisionController.Collision info);
        void CollisionExit(CollisionController.Collision info);
    }
}