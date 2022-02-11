using System.Collections;
using Interfaces.Physics;
using UnityEngine;

namespace Components
{
    public class BladeController : MonoBehaviour
    {
        [SerializeField] private InputController inputController;
        [SerializeField] private GameField2D gameField2D;
        [SerializeField] private GameObject bladePrefab;
        [Space]
        [SerializeField] private float minVelocity;

        private GameObject currentBlade;
        private BaseCollider currentBladeCollider;
        private Vector2 previousPosition;

        private void Start()
        {
            currentBlade = Instantiate(bladePrefab, gameField2D.transform);
            currentBladeCollider = currentBlade.GetComponent<BaseCollider>();
            currentBladeCollider.enabled = false;
        }

        private void OnEnable()
        {
            inputController.StartTouch += TouchStart;
            inputController.EndTouch += TouchEnd;
        }
        
        private void OnDisable()
        {
            inputController.StartTouch -= TouchStart;
            inputController.EndTouch -= TouchEnd;
        }

        private void TouchStart(Vector2 point)
        {
            currentBladeCollider.enabled = false;
            
            previousPosition = gameField2D.ScreenPointToGameField2D(point);
            
            StartCoroutine(nameof(BladeTrail));
        }

        private IEnumerator BladeTrail()
        {
            while (true)
            {
                Vector2 newPosition = gameField2D.ScreenPointToGameField2D(InputController.PrimaryPosition());
                
                currentBlade.transform.position = newPosition;
                
                IsSlice(newPosition);
                
                previousPosition = newPosition;
                
                yield return null;
            }
        }

        private void TouchEnd(Vector2 point)
        {
            currentBladeCollider.enabled = false; 
            
            StopCoroutine(nameof(BladeTrail));
        }

        private void IsSlice(Vector2 position)
        {
            var velocity = (position - previousPosition).magnitude * Time.deltaTime;

            if (velocity < minVelocity)
            {
                currentBladeCollider.enabled = false;
                return;
            }
            
            currentBladeCollider.enabled = true;
        }
    }
}