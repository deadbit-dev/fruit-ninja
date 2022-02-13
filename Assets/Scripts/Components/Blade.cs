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
           
            Slice(info.Collider.gameObject);
        }

        private void Slice(GameObject unit)
        {
             var unitTransform = unit.transform;
             var unitPosition = unitTransform.position;
             var unitRotation = unitTransform.rotation;
             var unitLocalScale = unitTransform.localScale;
             var sliceSprite = unit.GetComponent<SpriteRenderer>().sprite;
             
             // TODO: direction
             var sliceDirection = new Vector2();
 
             var (spriteA, spriteB) = RuntimeSpriteEditor.SliceSprite(sliceSprite, sliceDirection);
 
             var partA = Instantiate(partUnitPrefab, unitPosition, unitRotation);
             partA.transform.localScale = unitLocalScale;
             partA.GetComponent<SpriteRenderer>().sprite = spriteA;
             
             var partB = Instantiate(partUnitPrefab, unitPosition, unitRotation);
             partB.transform.localScale = unitLocalScale;
             partB.GetComponent<SpriteRenderer>().sprite = spriteB;
             
             SplatterController.Instance.InstanceSplatter(unitPosition, Color.white);
             
             Destroy(unit);
             
             // TODO: velocity for partA and partB by direction           
             
        }
    }
}