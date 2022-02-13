using System;
using UnityEngine;

namespace Controllers
{
    public class InputController : MonoBehaviour
    {
        public event Action<Vector2> StartTouch;
        public event Action<Vector2> EndTouch;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartTouchPrimary();
            }

            if (Input.GetMouseButtonUp(0))
            {
                EndTouchPrimary();
            }
        }

        private void StartTouchPrimary()
        {
            StartTouch?.Invoke(Input.mousePosition);
        }

        private void EndTouchPrimary()
        { 
            EndTouch?.Invoke(Input.mousePosition);
        }

        public static Vector2 PrimaryPosition()
        {
            return Input.mousePosition;
        }
    }
}