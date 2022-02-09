using System;
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
            
            Destroy(info.Collider.gameObject);
        }
    }
}