using Interfaces.Physics;
using UnityEngine;

namespace Components.Physics
{
    public class PhysicsUnit : MonoBehaviour
    {
        [SerializeField] private bool simulate;
        [Space] 
        [SerializeField] private float mass;
        [SerializeField] private Vector2 axisGravity2D;
        [SerializeField] private Vector2 velocity2D;
        [SerializeField] private float torque2D;

        private const float Gravity = 9.8f;
        
        public float Mass
        {
            get => mass;
            set => mass = value;
        }

        public Vector2 Velocity2D => velocity2D;
        public float Torque2D => torque2D;

        private void FixedUpdate()
        {
            if (!simulate)
            {
                return;
            }
            
            Simulate();
        }

        private void Simulate()
        {
            velocity2D += Gravity * mass * axisGravity2D * Time.fixedDeltaTime;
            transform.position += (Vector3) velocity2D * Time.fixedDeltaTime;
            transform.Rotate(new Vector3(0, 0,torque2D));
        }

        public void AddForce2D(Vector2 forceVelocity)
        {
            velocity2D += forceVelocity;
        }

        public void AddForce2D(float angleInRad, float force)
        {
            velocity2D += new Vector2(Mathf.Cos(angleInRad), Mathf.Sin(angleInRad)) * force;
        }

        public void AddTorque2D(float angle)
        {
            torque2D += angle;
        }
    }
}