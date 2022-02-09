using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Components
{
    public class InputController : MonoBehaviour
    {
        public event Action<Vector2> StartTouch;
        public event Action<Vector2> EndTouch;
        
        private Controls _controls;

        private void Awake()
        {
            _controls = new Controls();
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
           _controls.Disable(); 
        }

        private void Start()
        {
            _controls.Touch.PrimaryContact.started += StartTouchPrimary;
            _controls.Touch.PrimaryContact.canceled += EndTouchPrimary;
        }

        private void StartTouchPrimary(InputAction.CallbackContext context)
        {
            StartTouch?.Invoke(_controls.Touch.PrimaryPosition.ReadValue<Vector2>());
        }

        private void EndTouchPrimary(InputAction.CallbackContext context)
        { 
            EndTouch?.Invoke(_controls.Touch.PrimaryPosition.ReadValue<Vector2>());
        }

        public Vector2 PrimaryPosition()
        {
            return _controls.Touch.PrimaryPosition.ReadValue<Vector2>();
        }
    }
}