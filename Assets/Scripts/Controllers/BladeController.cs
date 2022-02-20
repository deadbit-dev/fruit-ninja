using System;
using System.Collections;
using UnityEngine;
using Components;
using Components.Physics;
using Controllers.Game;
using Interfaces.Physics;

namespace Controllers
{
    public class BladeController : MonoBehaviour
    {
        [SerializeField] private InputController inputController;
        [SerializeField] private GameController gameController;
        [SerializeField] private GameField2D gameField2D;
        [Space]
        [SerializeField] private GameObject bladePrefab;
        [Space]
        [SerializeField] private float minSpeed;

        private GameObject _bladeObject;
        private CircleCollider _bladeCollider;
        private Vector2 _previousPosition;

        public event Action<GameObject, Vector3> OnBladeContact;

        private void Start()
        {
            _bladeObject = Instantiate(bladePrefab, gameField2D.transform);
            _bladeCollider = _bladeObject.GetComponent<CircleCollider>();
            _bladeCollider.CollisionEnter += BladeCollisionEnter;
        }

        private void OnEnable()
        {
            gameController.OnStart += OnStart;
            gameController.OnEnd += OnEnd;
        }
        
        private void OnDisable()
        {
            gameController.OnStart -= OnStart;
            gameController.OnEnd -= OnEnd;
        }

        private void OnStart()
        {
            inputController.StartTouch += TouchStart;
            inputController.EndTouch += TouchEnd;

            if (_bladeObject == null)
            {
                return;
            }
            
            _bladeObject.SetActive(true);
        }

        private void OnEnd()
        { 
            inputController.StartTouch -= TouchStart;
            inputController.EndTouch -= TouchEnd; 
            
            _bladeObject.SetActive(false);
        }

        private void BladeCollisionEnter(ICollision info)
        {
            if (info.Collider == null)
            {
                return;
            }
            
            OnBladeContact?.Invoke(info.Collider.gameObject, info.ContactPosition);
        }

        private void TouchStart(Vector2 point)
        {
            _previousPosition = gameField2D.ScreenPointToGameField2D(point);
            _bladeObject.SetActive(true);
            _bladeCollider.enabled = false;
            
            StartCoroutine(BladeTrail());
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

        private void IsSlice(Vector2 position)
        {
            var speed = (position - _previousPosition).magnitude * Time.deltaTime;

            if (speed < minSpeed)
            {
                _bladeCollider.enabled = false;
                return;
            }
            
            _bladeCollider.enabled = true;
        }
        
        private void TouchEnd(Vector2 point)
        {
            _bladeObject.SetActive(false);
        }
    }
}