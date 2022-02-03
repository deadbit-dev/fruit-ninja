using UnityEngine;

namespace Interfaces
{
    public interface ISpawner
    {
        void Spawn();
        
        void SetMinPoint(Vector3 point);
        void SetMaxPoint(Vector3 point);
        void SetAngleMinPoint(float angle);
        void SetForceMinPoint(float force);
        void SetAngleMaxPoint(float angle);
        void SetForceMaxPoint(float force);
    }
}