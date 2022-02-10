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

        private GameObject _currentBlade;
        private BaseCollider _currentBladeCollider;
        private Vector2 _previousPosition;
        
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
            _currentBlade = Instantiate(bladePrefab, gameField2D.transform);

            _currentBladeCollider = _currentBlade.GetComponent<BaseCollider>();
            _currentBladeCollider.enabled = false;
            
            _previousPosition = gameField2D.ScreenPointToGameField2D(point);
            
            StartCoroutine(nameof(BladeTrail));
        }

        private IEnumerator BladeTrail()
        {
            while (true)
            {
                Vector2 newPosition = gameField2D.ScreenPointToGameField2D(inputController.PrimaryPosition());
                
                _currentBlade.transform.position = newPosition;
                
                IsCut(newPosition);
                
                _previousPosition = newPosition;
                
                yield return null;
            }
        }

        private void TouchEnd(Vector2 point)
        {
            const float delayDestroyBlade = 0.2f;
            Destroy(_currentBlade, delayDestroyBlade);
            
            StopCoroutine(nameof(BladeTrail));
        }

        private void IsCut(Vector2 position)
        {
            var velocity = (position - _previousPosition).magnitude * Time.deltaTime;

            if (velocity < minVelocity)
            {
                _currentBladeCollider.enabled = false;
                return;
            }
            
            _currentBladeCollider.enabled = true;
        }
    }
}