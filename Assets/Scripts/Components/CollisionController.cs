using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Components
{
    public class CollisionController : MonoBehaviour
    {
        [SerializeField] private int countCollisionUnits;
        
        private List<BaseCollider> _colliders;
    
        public struct Collision
        {
            public BaseCollider colliderA;
            public BaseCollider colliderB;
            public Vector3 collisionPoint;
        }

        private void Start()
        {
            _colliders = new List<BaseCollider>(countCollisionUnits);
        }

        private void FixedUpdate()
        {
            // TODO: check collisions enter/exit for each colliders 
        }

        public void AddCollider(BaseCollider col)
        {
            _colliders.Add(col);
        }

        public void RemoveCollider(BaseCollider col)
        {
            _colliders.Remove(col);
        }
    }
}