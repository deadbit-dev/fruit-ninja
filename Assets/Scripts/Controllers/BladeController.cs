using System.Collections;
using UnityEngine;
using Components;

namespace Controllers
{
    public class BladeController : MonoBehaviour
    {
        [SerializeField] private InputController inputController;
        [SerializeField] private GameField2D gameField2D;
        [SerializeField] private GameObject bladePrefab;
        [Space]
        [SerializeField] private float minSpeed;

        private GameObject bladeObject;
        private TrailRenderer bladeTrail;
        private Blade bladeScript;
        private Vector2 previousPosition;

        private void Start()
        {
            bladeObject = Instantiate(bladePrefab, gameField2D.transform);
            bladeTrail = bladeObject.GetComponent<TrailRenderer>();
            bladeScript = bladeObject.GetComponent<Blade>();
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
            bladeTrail.enabled = true;
            
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
            bladeTrail.enabled = false; 
            
            StopCoroutine(nameof(BladeTrail));
        }

        private void IsSlice(Vector2 position)
        {
            var speed = (position - previousPosition).magnitude * Time.deltaTime;

            if (speed < minSpeed)
            {
                bladeScript.enabled = false;
                return;
            }
            
            bladeScript.enabled = true;
        }
    }
}