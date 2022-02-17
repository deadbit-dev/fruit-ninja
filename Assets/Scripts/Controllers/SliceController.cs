using Components.Physics;
using UnityEngine;
using Utils;

namespace Controllers
{
    public class SliceController : MonoBehaviour
    {
        [SerializeField] private float force;
        [SerializeField] private float torque;

        public void Slice(GameObject unit, Vector3 contact)
        {
            var transformUnit = unit.transform;
            var transformParentUnit = transformUnit.parent;
            var sliceSprite = unit.GetComponent<SpriteRenderer>().sprite;

            var contactUV = RuntimeSpriteEditor.WorldPointToSpriteUV(sliceSprite, contact, transformUnit);
            var sliceDirection = RuntimeSpriteEditor.SliceDirectionByContact(sliceSprite, contactUV);
            var smartSliceAngle = RuntimeSpriteEditor.SmartSliceAngleBySliceDirection(sliceSprite, sliceDirection);

            var spriteA = Instantiate(sliceSprite);
            var spriteB = Instantiate(sliceSprite);
            
            RuntimeSpriteEditor.SpriteSliceBySmartAngle(sliceSprite, ref spriteA, ref spriteB, smartSliceAngle);
 
            var partA = Instantiate(unit, transformParentUnit);
            partA.GetComponent<SpriteRenderer>().sprite = spriteA;
            partA.tag = "Part";
            
            var partB = Instantiate(unit, transformParentUnit);
            partB.GetComponent<SpriteRenderer>().sprite = spriteB;
            partB.tag = "Part";
            
            Destroy(unit);

            var sliceDirectionImpulse = Vector2.Perpendicular(Math.VectorByAngle(smartSliceAngle)) * force;
            
            var partPhysicsUnit = partA.GetComponent<PhysicsUnit>();
            
            var partTorque = partPhysicsUnit.Torque2D + torque;
            
            partPhysicsUnit.AddForce2D(-sliceDirectionImpulse);
            partPhysicsUnit.AddTorque2D(-partTorque);
            
            partPhysicsUnit = partB.GetComponent<PhysicsUnit>();
            
            partPhysicsUnit.AddForce2D(sliceDirectionImpulse);
            partPhysicsUnit.AddTorque2D(-partTorque);
        }    
    }
}