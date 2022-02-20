using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class ExplosionController : MonoBehaviour
    {
        [SerializeField] private BladeController bladeController;
        [SerializeField] private List<string> tagsUnitForExplosion;
        [SerializeField] private float radiusExplosion;

        public event Action<GameObject> OnExplosion;

        private void OnEnable()
        {
            bladeController.OnBladeContact += Explosion;
        }

        private void OnDisable()
        {
            bladeController.OnBladeContact -= Explosion;
        }

        private void Explosion(GameObject unit, Vector3 contactPosition)
        {
            if(!tagsUnitForExplosion.Contains(unit.tag.ToString())) return;
            
            Debug.Log("Explosion");
            
            OnExplosion?.Invoke(unit);
            
            PhysicsController.Explosion(unit.transform, radiusExplosion);

            Destroy(unit);
        }
    }
}