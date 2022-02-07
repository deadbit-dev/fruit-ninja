using UnityEngine;
using Components.Physics;
using Components.Utils;
using Interfaces.Physics;

namespace Components
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private CollisionSpace collisionSpace;
        [Space]
        [SerializeField] private Camera screenSpace;
        [Space]
        [SerializeField] private ViewportField viewportField;

        private void Start()
        {
            collisionSpace.Offset = (Vector2) Center - (collisionSpace.Center - collisionSpace.Offset);
            collisionSpace.Size = Size; 
            collisionSpace.CollisionExit += CollisionExit;
        }
        
        private static void CollisionExit(ICollision info)
        {
            Destroy(info.Collider.gameObject);
        }

        public Vector2 ViewportPointToScreenPoint(ViewportPoint viewportPoint)
        {
            if (screenSpace == null)
            {
                return (Vector2) viewportPoint;
            }

            return screenSpace.ViewportToScreenPoint((Vector2) viewportPoint);
        }
        
        public Vector3 ViewportPointToWorldPoint(ViewportPoint viewportPoint)
        {
            if (screenSpace == null)
            {
                return (Vector2) viewportPoint;
            }
        
            var worldPoint = screenSpace.ViewportToWorldPoint((Vector2) viewportPoint);
            worldPoint.z = transform.position.z;
            return worldPoint;
        }

        public Vector3 Center => ViewportPointToWorldPoint(viewportField.CenterPoint);
        public Vector3 Min => ViewportPointToWorldPoint(viewportField.MinPoint);
        public Vector3 Max => ViewportPointToWorldPoint(viewportField.MaxPoint);
        public Vector3 Size => Max - Min;

#if UNITY_EDITOR        
        private void OnDrawGizmos()
        {
            if (viewportField == null || collisionSpace == null)
            {
                return;
            }
            
            collisionSpace.Offset = (Vector2) Center - (collisionSpace.Center - collisionSpace.Offset);
            collisionSpace.Size = Size;
        }
#endif
 
    }
}