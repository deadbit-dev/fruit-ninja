using Interfaces;
using UnityEngine;

namespace Components
{
    public class PhysicsUnit : BaseUnit
    {
        [SerializeField] private Vector3 velocity;

        public Vector3 Velocity
        {
            get => velocity;
            set => velocity = value;
        }

        public override void Move()
        {
            
        }
    }
}