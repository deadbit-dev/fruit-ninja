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
        }

        private void Slice(GameObject unit, Vector3 contactSlice)
        {
            var unitTransform = unit.transform;
            var unitPosition = unitTransform.position;
            var unitRotation = unitTransform.rotation;
            var unitLocalScale = unitTransform.lossyScale;
            var parentUnit = unitTransform.parent;
            var sliceSprite = unit.GetComponent<SpriteRenderer>().sprite;

            var contactSliceUV = RuntimeSpriteEditor.WorldPointToSpriteUV(sliceSprite, contactSlice, unitTransform);
            var spritePivotUV = RuntimeSpriteEditor.SpritePivotUV(sliceSprite);

            var sliceDirection = (contactSliceUV - spritePivotUV).normalized;

            var (spriteA, spriteB) = RuntimeSpriteEditor.SpriteSliceByPivot(sliceSprite, sliceDirection);
 
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
                         
            // TODO: velocity for partA and partB by sliceSecant
        }
    }
}