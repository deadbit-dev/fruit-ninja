using Interfaces;
using UnityEngine;

namespace Components
{
    public class CircleCollider : BaseCollider
    {
        [SerializeField] private Vector2 point;
        [SerializeField] private float radius;

        private CollisionController _collisionController;
        
        private void Start()
        {
            _collisionController = GameObject.Find("CollisionController").GetComponent<CollisionController>();
            _collisionController.AddCollider(this);
        }

        private void OnDestroy()
        {
           _collisionController.RemoveCollider(this); 
        }

        public override void CollisionEnter(CollisionController.Collision info)
        {
            
        }

        public override void CollisionExit(CollisionController.Collision info)
        {
            
        }
    }
}