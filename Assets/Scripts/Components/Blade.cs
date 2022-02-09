using System.Collections;
using UnityEngine;

namespace Components
{
    public class Blade : MonoBehaviour
    {
        [SerializeField] private InputController inputController;
        [SerializeField] private GameField2D gameField2D;
        [SerializeField] private TrailRenderer trail;
        [Space]
        [SerializeField] private float minDistance;

        private Vector2 _startPoint;
        private Vector2 _endPoint;
        
        private void OnEnable()
        {
            inputController.StartTouch += SwipeStart;
            inputController.EndTouch += SwipeEnd;
        }
        
        private void OnDisable()
        {
            inputController.StartTouch -= SwipeStart;
            inputController.EndTouch -= SwipeEnd;
        }

        private void SwipeStart(Vector2 point)
        {
            _startPoint = gameField2D.ScreenPointToGameField2D(point);
            transform.position = _startPoint;
            StartCoroutine(nameof(BladeTrail));
        }

        private IEnumerator BladeTrail()
        {
            while (true)
            {
                _endPoint = gameField2D.ScreenPointToGameField2D(inputController.PrimaryPosition());
                transform.position = _endPoint;
                yield return null;
            }
        }

        private void SwipeEnd(Vector2 point)
        {
            StopCoroutine(nameof(BladeTrail));
            _endPoint = gameField2D.ScreenPointToGameField2D(point);
            IsSwipe();
        }

        private void IsSwipe()
        {
            if (Vector3.Distance(_startPoint, _endPoint) >= minDistance)
            {
                Debug.Log("Swipe");
            }
        }
    }
}