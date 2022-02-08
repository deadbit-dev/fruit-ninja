using UnityEngine;
using Components.Physics;

namespace Components
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private CircleCollider circleCollider;
        [SerializeField] private PhysicsUnit physics;
        [Space]
        [SerializeField] private float rotateAnimationAngleMin;
        [SerializeField] private float rotateAnimationAngleMax;

        private void Start()
        {
            physics.AddTorque2D(Random.Range(rotateAnimationAngleMin, rotateAnimationAngleMax));
        }
    }
}