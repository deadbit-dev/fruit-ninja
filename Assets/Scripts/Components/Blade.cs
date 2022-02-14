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
            var unitLocalScale = unitTransform.localScale;
            var sliceSprite = unit.GetComponent<SpriteRenderer>().sprite;

            var contactSliceUV = RuntimeSpriteEditor.WorldPointToSpriteUV(sliceSprite, contactSlice, unitTransform);
            var spritePivotUV = RuntimeSpriteEditor.SpritePivotUV(sliceSprite);

            // TODO: sliceSecant by contactSliceUV and spritePivotUV
            var sliceSecant = new Vector2();

            var (spriteA, spriteB) = RuntimeSpriteEditor.SpriteSlice(sliceSprite, sliceSecant);
 
            var partA = Instantiate(partUnitPrefab, unitPosition, unitRotation);
            partA.transform.localScale = unitLocalScale;
            partA.GetComponent<SpriteRenderer>().sprite = spriteA;
            
            var partB = Instantiate(partUnitPrefab, unitPosition, unitRotation);
            partB.transform.localScale = unitLocalScale;
            partB.GetComponent<SpriteRenderer>().sprite = spriteB;
             
            SplatterController.Instance.InstanceSplatter(unitPosition, Color.white);
                         
            Destroy(unit);
                         
            // TODO: velocity for partA and partB by sliceSecant
        }
    }
}