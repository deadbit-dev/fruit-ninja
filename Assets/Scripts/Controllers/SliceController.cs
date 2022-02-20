using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Controllers
{
    public class SliceController : MonoBehaviour
    {
        [SerializeField] private BladeController bladeController;
        [Space]
        [SerializeField] private List<string> tagsObjectsForSlicing;

        public event Action<GameObject> OnSlice;

        private void OnEnable()
        {
            bladeController.OnBladeContact += BladeContact;
        }
        private void OnDisable()
        {
            bladeController.OnBladeContact -= BladeContact;
        }

        private void BladeContact(GameObject unit, Vector3 contactPosition)
        {
            if (!tagsObjectsForSlicing.Contains(unit.tag.ToString())) return;
            
            OnSlice?.Invoke(unit); 
            Slicer.SliceObject(unit, contactPosition);
        }
    }
}