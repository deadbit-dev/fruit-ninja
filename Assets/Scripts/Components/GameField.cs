using UnityEngine;
using Components.Physics;
using Components.Utils;
using Interfaces.Physics;

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
        
        private static void CollisionExit(ICollision info)
        {
            Destroy(info.Collider.gameObject);
        }
        
#if UNITY_EDITOR        
        private void OnDrawGizmosSelected()
        {
            if (viewportField == null)
            {
                return;
            }
            
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireCube(
                viewportField.CenterPointFieldToWorld,
                viewportField.MaxPointFieldToWorld - viewportField.MinPointFieldToWorld);
        }
#endif
 
    }
}