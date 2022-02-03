using UnityEngine;

namespace Interfaces
{
    public abstract class BaseSpawner: MonoBehaviour, ISpawner
    {
        public abstract void Spawn();
        
        public abstract void SetMinPoint(Vector3 point);
        public abstract void SetMaxPoint(Vector3 point);
        public abstract void SetAngleMinPoint(float angle);
        public abstract void SetForceMinPoint(float force);
        public abstract void SetAngleMaxPoint(float angle);
        public abstract void SetForceMaxPoint(float force);
    }
}