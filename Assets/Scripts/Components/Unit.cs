using UnityEngine;

namespace Components
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private CircleCollider circleCollider;
        [SerializeField] private PhysicsUnit physics;
    }
}