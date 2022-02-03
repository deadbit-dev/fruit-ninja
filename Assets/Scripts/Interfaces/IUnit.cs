using UnityEngine;

namespace Interfaces
{
    public interface IUnit
    {
        void Move();
        void SetVelocity(Vector3 velocity);
    }
}