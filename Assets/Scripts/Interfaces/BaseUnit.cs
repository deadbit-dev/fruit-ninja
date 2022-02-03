using UnityEngine;

namespace Interfaces
{
    public abstract class BaseUnit: MonoBehaviour, IUnit
    {
        private void Update()
        {
            Move();
        }

        public abstract void Move();
    }
}