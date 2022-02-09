using System;
using UnityEngine;
using Components.Physics;
using Components.Utils;
using Interfaces.Physics;

namespace Components
{
    public class GameField2D : MonoBehaviour
    {
        [SerializeField] private CollisionSpace2D collisionSpace2D;
        [Space]
        [SerializeField] private Camera screenSpace;
        [Space]
        [SerializeField] private ViewportField viewportField;

        private void Awake()
        {
            SetSizeCollisionSpace2D();
        }

        private void OnEnable()
        {
            collisionSpace2D.CollisionExit += CollisionExit;
        }

        private void OnDisable()
        {
            collisionSpace2D.CollisionExit -= CollisionExit;
        }

        private static void CollisionExit(ICollision info)
        {
            Destroy(info.Collider.gameObject);
        }

        private void SetSizeCollisionSpace2D()
        {
            collisionSpace2D.Offset = (Vector2) screenSpace.ViewportToWorldPoint((Vector2) viewportField.Center) -
                                      (collisionSpace2D.Center - collisionSpace2D.Offset);
            
            collisionSpace2D.Size = screenSpace.ViewportToWorldPoint((Vector2) viewportField.Max) - 
                                    screenSpace.ViewportToWorldPoint((Vector2) viewportField.Min);
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
            if (collisionSpace2D == null || screenSpace == null)
            {
                return;
            }
            
            SetSizeCollisionSpace2D();
        }
#endif
 
    }
}