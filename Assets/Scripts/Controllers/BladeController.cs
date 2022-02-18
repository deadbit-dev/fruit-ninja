using System;
using System.Collections;
using UnityEngine;
using Components;
using Interfaces.Physics;

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
        private BaseCollider _bladeCollider;
        private Vector2 _previousPosition;
        private Coroutine _bladeTrail;

        public event Action<GameObject, Vector3> OnBladeContact;

        private void Start()
        {
            _bladeObject = Instantiate(bladePrefab, gameField2D.transform);
            _bladeCollider = _bladeObject.GetComponent<BaseCollider>();
            _bladeCollider.CollisionEnter += BladeCollisionEnter;
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
            _bladeObject.SetActive(true);
            _bladeCollider.enabled = false;
            
            _bladeTrail = StartCoroutine(BladeTrail());
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
            
            StopCoroutine(_bladeTrail);
        }
    }
}