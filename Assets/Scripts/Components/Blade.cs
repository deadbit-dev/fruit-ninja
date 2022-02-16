using Components.Physics;
using UnityEngine;
using Utils;
using Interfaces.Physics;
using Controllers;

namespace Components
{
    public class Blade : MonoBehaviour
    {
        [SerializeField] private BaseCollider bladeCollider;
        [SerializeField] private GameObject partUnitPrefab;
        [SerializeField] private float forceSlice;

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
            if (info.Collider == null || !info.Collider.gameObject.CompareTag("Unit"))
            {
                return;
            }
           
            Slice(info.Collider.gameObject, info.ContactPosition);
            
            ScoreController.Instance.SetScore(info.Collider.gameObject.GetComponent<Score>());
        }

        private void Slice(GameObject unit, Vector3 contactSlice)
        {
            var unitTransform = unit.transform;
            var unitPosition = unitTransform.position;
            var unitRotation = unitTransform.rotation;
            var unitLocalScale = unitTransform.lossyScale;
            var unitPhysics = unit.GetComponent<PhysicsUnit>();
            var unitVelocity = unitPhysics.Velocity;
            var unitTorque = unitPhysics.Torque2D;
            
            var sliceSprite = unit.GetComponent<SpriteRenderer>().sprite;

            var contactSliceUV = RuntimeSpriteEditor.WorldPointToSpriteUV(sliceSprite, contactSlice, unitTransform);
            var spritePivotUV = RuntimeSpriteEditor.SpritePivotUV(sliceSprite);

            var sliceDirection = (contactSliceUV - spritePivotUV).normalized;

            var smartSliceAngle = RuntimeSpriteEditor.SmartSliceAngleBySliceDirection(sliceSprite, sliceDirection);

            var (spriteA, spriteB) = RuntimeSpriteEditor.SpriteSliceBySmartAngle(sliceSprite, smartSliceAngle);
 
            var parentUnit = unitTransform.parent;
            
            var partA = Instantiate(partUnitPrefab, parentUnit);
            partA.GetComponent<Transform>().position = unitPosition;
            partA.transform.rotation = unitRotation;
            partA.transform.localScale = unitLocalScale;
            partA.GetComponent<SpriteRenderer>().sprite = spriteA;
            
            var partB = Instantiate(partUnitPrefab, parentUnit);
            partB.GetComponent<Transform>().position = unitPosition;
            partB.transform.rotation = unitRotation;
            partB.transform.localScale = unitLocalScale;
            partB.GetComponent<SpriteRenderer>().sprite = spriteB;

            EffectController.Instance.InstanceSplatter(unitPosition, Color.white);
                         
            Destroy(unit);

            var sliceDirectionImpulse = Vector2.Perpendicular(Math.VectorByAngle(smartSliceAngle)) * forceSlice;
            
            var partPhysicsUnit = partA.GetComponent<PhysicsUnit>();
            partPhysicsUnit.AddForce2D(unitVelocity - (Vector3) sliceDirectionImpulse);
            partPhysicsUnit.AddTorque2D(unitTorque);
            
            partPhysicsUnit = partA.GetComponent<PhysicsUnit>();
            partPhysicsUnit.AddForce2D(unitVelocity + (Vector3) sliceDirectionImpulse);
            partPhysicsUnit.AddTorque2D(unitTorque);           
        }
    }
}