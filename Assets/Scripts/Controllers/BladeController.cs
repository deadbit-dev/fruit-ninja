using System.Collections;
using UnityEngine;
using Interfaces.Physics;
using Components;

namespace Controllers
{
    public class BladeController : MonoBehaviour
    {
        [SerializeField] private InputController inputController;
        [SerializeField] private GameField2D gameField2D;
        [SerializeField] private GameObject bladePrefab;
        [Space]
        [SerializeField] private float minVelocity;

        private GameObject bladeObject;
        private Blade bladeScript;
        private TrailRenderer currentBladeTrail;
        private Vector2 previousPosition;

        private void Start()
        {
            bladeObject = Instantiate(bladePrefab, gameField2D.transform);
            bladeScript = bladeObject.GetComponent<Blade>();
            currentBladeTrail = bladeObject.GetComponent<TrailRenderer>();
            bladeScript.enabled = false;
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
            previousPosition = gameField2D.ScreenPointToGameField2D(point);
            currentBladeTrail.enabled = true;
            
            StartCoroutine(nameof(BladeTrail));
        }

        private IEnumerator BladeTrail()
        {
            while (true)
            {
                Vector2 newPosition = gameField2D.ScreenPointToGameField2D(InputController.PrimaryPosition());
                
                bladeObject.transform.position = newPosition;
                
                IsSlice(newPosition);
                
                previousPosition = newPosition;
                
                yield return null;
            }
        }

        private void TouchEnd(Vector2 point)
        {
            bladeScript.enabled = false;
            currentBladeTrail.enabled = false; 
            
            StopCoroutine(nameof(BladeTrail));
        }

        private void IsSlice(Vector2 position)
        {
            var velocity = (position - previousPosition).magnitude * Time.deltaTime;

            if (velocity < minVelocity)
            {
                bladeScript.enabled = false;
                return;
            }
            
            bladeScript.enabled = true;
        }
    }
}