using System;
using UnityEngine;

namespace Components
{
    public class PhysicsUnit : MonoBehaviour
    {
        [SerializeField] private bool simulate;
        [Space] [SerializeField] private float mass;
        [SerializeField] private Vector3 velocity;

        private const float Gravity = 9.8f;

        private void FixedUpdate()
        {
            if (!simulate) return;
            Simulate();
        }

        private void Simulate()
        {
            velocity -= new Vector3(0, Gravity * mass, 0);
            transform.position += velocity * Time.fixedDeltaTime;
        }

        public void AddForce2D(float angleInRad, float force)
        {
            velocity += new Vector3(Mathf.Cos(angleInRad), Mathf.Sin(angleInRad)) * force;
        }
    }
}