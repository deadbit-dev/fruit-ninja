using Interfaces.Physics;
using UnityEngine;

namespace Components.Physics
{
    [RequireComponent(typeof(BaseCollider))]
    public class PhysicsUnit : MonoBehaviour
    {
        [SerializeField] private BaseCollider body;
        [Space]
        [SerializeField] private bool simulate;
        [Space] 
        [SerializeField] private float mass;
        [SerializeField] private Vector3 axisGravity;
        [SerializeField] private Vector3 velocity;
        [SerializeField] private Vector3 torque;

        private const float Gravity = 9.8f;
        
        public float Mass
        {
            get => mass;
            set => mass = value;
        }

        public Vector3 Velocity => velocity;
        public float Torque2D => torque.z;

        private void FixedUpdate()
        {
            if (!simulate)
            {
                return;
            }
            
            Simulate();
            Torque();
        }

        private void Simulate()
        {
            velocity += axisGravity * (Gravity * mass) * Time.fixedDeltaTime;
            transform.position += velocity * Time.fixedDeltaTime;
        }

        private void Torque()
        {
            transform.Rotate(torque);
        }

        public void AddForce2D(Vector3 forceVelocity)
        {
            velocity += forceVelocity;
        }

        public void AddForce2D(float angleInRad, float force)
        {
            velocity += new Vector3(Mathf.Cos(angleInRad), Mathf.Sin(angleInRad)) * force;
        }

        public void AddTorque2D(float angle)
        {
            torque.z += angle;
        }
    }
}