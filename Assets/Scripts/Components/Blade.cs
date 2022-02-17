using System;
using Components.Physics;
using UnityEngine;
using Utils;
using Interfaces.Physics;
using Controllers;
using Math = Utils.Math;

namespace Components
{
    public class Blade : MonoBehaviour
    {
        [SerializeField] private BaseCollider bladeCollider;

        public event Action<GameObject, Vector3> OnContactWithUnit;

        private void OnEnable()
        {
            bladeCollider.CollisionEnter += CollisionEnter;
        }

        private void OnDisable()
        {
            bladeCollider.CollisionEnter -= CollisionEnter;
        }

        private void CollisionEnter(ICollision info)
        {
            if (info.Collider == null)
            {
                return;
            }

            OnContactWithUnit?.Invoke(info.Collider.gameObject, info.ContactPosition);
        }
    }
}