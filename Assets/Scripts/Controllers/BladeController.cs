using System;
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

        private GameObject _bladeObject;
        private TrailRenderer _bladeTrail;
        private Vector2 _previousPosition;
        private Blade _bladeScript;

        public event Action<GameObject, Vector3> OnBladeContactWithUnit;

        private void Start()
        {
            _bladeObject = Instantiate(bladePrefab, gameField2D.transform);
            _bladeTrail = _bladeObject.GetComponent<TrailRenderer>();
            _bladeScript = _bladeObject.GetComponent<Blade>();
            _bladeScript.OnContactWithUnit += BladeContactWithUnitEvent;
            _bladeScript.enabled = false;
        }
        
        private void BladeContactWithUnitEvent(GameObject unit, Vector3 contactPosition)
        {
            OnBladeContactWithUnit?.Invoke(unit, contactPosition);
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
            _previousPosition = gameField2D.ScreenPointToGameField2D(point);
            _bladeTrail.enabled = true;
            
            StartCoroutine(nameof(BladeTrail));
        }

        private IEnumerator BladeTrail()
        {
            while (enabled)
            {
                Vector2 newPosition = gameField2D.ScreenPointToGameField2D(InputController.PrimaryPosition());
               
                _bladeObject.transform.position = newPosition;
                
                IsSlice(newPosition);
                
                _previousPosition = newPosition;
                
                yield return null;
            }
        }

        private void TouchEnd(Vector2 point)
        {
            _bladeScript.enabled = false;
            _bladeTrail.enabled = false; 
            
            StopCoroutine(nameof(BladeTrail));
        }

        private void IsSlice(Vector2 position)
        {
            var speed = (position - _previousPosition).magnitude * Time.deltaTime;

            if (speed < minSpeed)
            {
                _bladeScript.enabled = false;
                return;
            }
            
            _bladeScript.enabled = true;
        }
    }
}