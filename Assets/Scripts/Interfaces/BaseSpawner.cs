using UnityEngine;

namespace Interfaces
{
    public abstract class BaseSpawner: MonoBehaviour, ISpawner
    {
        public abstract void Spawn();
    }
}