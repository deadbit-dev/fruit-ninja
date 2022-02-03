using Interfaces;
using UnityEngine;

namespace Components
{
    public class Spawner : BaseSpawner
    {
        [SerializeField] private Vector3 minPoint;
        [SerializeField] private Vector3 maxPoint;
        [SerializeField] private float angleMinPoint;
        [SerializeField] private float forceMinPoint;
        [SerializeField] private float angleMaxPoint;
        [SerializeField] private float forceMaxPoint;
        
        [SerializeField] private BaseUnit unitPrefab;

        public override void Spawn()
        {
            var position = Utils.RandomRangeVector3(minPoint, maxPoint);
            
            // TODO: get weight by point between points
            const float weightVelocity = 0.5f;
            
            var velocity = Vector3.Lerp( 
                Utils.GetVelocity(angleMinPoint, forceMinPoint), 
                Utils.GetVelocity(angleMaxPoint, forceMaxPoint), 
                weightVelocity);

            Instantiate(unitPrefab, position, Quaternion.identity).GetComponent<BaseUnit>().SetVelocity(velocity);
        }

        public override void SetMinPoint(Vector3 point)
        {
            minPoint = point;
        }

        public override void SetMaxPoint(Vector3 point)
        {
            maxPoint = point;
        }

        public override void SetAngleMinPoint(float angle)
        {
            angleMinPoint = angle;
        }

        public override void SetForceMinPoint(float force)
        {
            forceMinPoint = force;
        }

        public override void SetAngleMaxPoint(float angle)
        {
            angleMaxPoint = angle;
        }

        public override void SetForceMaxPoint(float force)
        {
            forceMaxPoint = force;
        }
    }
}