using Components.Physics;
using UnityEngine;

namespace Utils
{
    public static class Slicer
    {
        public static void Slice(GameObject unit, Vector3 contact)
        {
            var transformUnit = unit.transform;
            var transformParentUnit = transformUnit.parent;
            var sliceSprite = unit.GetComponent<SpriteRenderer>().sprite;

            var contactUV = RuntimeSpriteEditor.WorldPointToSpriteUV(sliceSprite, contact, transformUnit);
            var sliceDirection = RuntimeSpriteEditor.SliceDirectionByContact(sliceSprite, contactUV);
            var smartSliceAngle = RuntimeSpriteEditor.SmartSliceAngleBySliceDirection(sliceSprite, sliceDirection);

            var (spriteA, spriteB) = RuntimeSpriteEditor.SpriteSliceBySmartAngle(sliceSprite, smartSliceAngle);
 
            var partA = Object.Instantiate(unit, transformParentUnit);
            partA.GetComponent<SpriteRenderer>().sprite = spriteA;
            partA.tag = "Part";
            
            var partB = Object.Instantiate(unit, transformParentUnit);
            partB.GetComponent<SpriteRenderer>().sprite = spriteB;
            partB.tag = "Part";
            
            Object.Destroy(unit);
            
            // TODO: move bellow logic to explosion effector
             
            const float force = 1f;
            const float torque = 3f;
            
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