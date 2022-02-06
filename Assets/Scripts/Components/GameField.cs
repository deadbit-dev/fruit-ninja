using UnityEngine;
using Components.Physics;
using Collision = Components.Physics.Collision;

namespace Components
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private ViewportField viewportField;
        [SerializeField] private CollisionSpace collisionSpace;

        public ViewportField ViewportField => viewportField;

        private void Start()
        {
            collisionSpace.Max = viewportField.MaxPointFieldToWorld;
            collisionSpace.CollisionExit += CollisionExit;
        }
        
        private static void CollisionExit(Collision info)
        {
            Destroy(info.Collider.gameObject);
        }

    }
}