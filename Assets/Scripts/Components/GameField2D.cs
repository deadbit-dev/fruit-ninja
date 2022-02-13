using UnityEngine;
using Utils;
using Interfaces.Physics;
using Components.Physics;

namespace Components
{
    public class GameField2D : MonoBehaviour
    {
        [SerializeField] private Camera screenSpace;
        [Space]
        [SerializeField] private ViewportField viewportField;
        [Space]
        [SerializeField] private SpaceCollider2D spaceCollider2D;
        [Space] 
        [SerializeField] private Vector3 increaseSizeCollisionSpace2D;

        private void Awake()
        {
            SetSizeCollisionSpace2D();
        }

        private void OnEnable()
        {
            spaceCollider2D.CollisionExit += CollisionExit;
        }

        private void OnDisable()
        {
            spaceCollider2D.CollisionExit -= CollisionExit;
        }

        private static void CollisionExit(ICollision info)
        {
            if (info.Collider == null)
            {
                return;
            }

            Destroy(info.Collider.gameObject);
        }

        private void SetSizeCollisionSpace2D()
        {
            spaceCollider2D.Offset = (Vector2) screenSpace.ViewportToWorldPoint((Vector2) viewportField.Center) -
                                      (spaceCollider2D.Center - spaceCollider2D.Offset);

            spaceCollider2D.Size = screenSpace.ViewportToWorldPoint((Vector2) viewportField.Max) - 
                                    screenSpace.ViewportToWorldPoint((Vector2) viewportField.Min) +
                                    increaseSizeCollisionSpace2D;
        }

        public Vector3 ViewportPointToGameField2D(ViewportPoint viewportPoint)
        {
            if (screenSpace == null)
            {
                return (Vector2) viewportPoint;
            }
            
            var worldPoint2D = screenSpace.ViewportToWorldPoint((Vector2) (viewportField.Size * viewportPoint + viewportField.Min));
            worldPoint2D.z = transform.position.z;
            return worldPoint2D;
        }

        public Vector3 ScreenPointToGameField2D(Vector2 point)
        {
            var worldPoint2D = screenSpace.ScreenToWorldPoint(point);
            worldPoint2D.z = transform.position.z;
            return worldPoint2D;
        }

#if UNITY_EDITOR        
        private void OnDrawGizmosSelected()
        {
            if (spaceCollider2D == null || screenSpace == null)
            {
                return;
            }
            
            SetSizeCollisionSpace2D();
        }
#endif
 
    }
}