using UnityEngine;

namespace Interfaces
{
    public abstract class BaseUnit: MonoBehaviour, IUnit
    {
        public abstract void Move();
        public abstract void SetVelocity(Vector3 velocity);
    }
}