using Components.Physics;
using Interfaces.Physics;
using UnityEngine;

namespace Components
{
    public class Blade : MonoBehaviour
    {
        [SerializeField] private CircleCollider circleCollider;

        private void OnEnable()
        {
            circleCollider.CollisionEnter += CollisionEnter;
        }

        private void OnDisable()
        {
            circleCollider.CollisionEnter -= CollisionEnter;
        }

        private static void CollisionEnter(ICollision info)
        {
            if (info.Collider == null)
            {
                return;
            }
            
            SliceController.Instance.SliceUnit(info.Collider.gameObject);
            SplatterController.Instance.InstanceSplatter(info.Collider.transform.position, Color.white);
           
            Destroy(info.Collider.gameObject);
        }
    }
}