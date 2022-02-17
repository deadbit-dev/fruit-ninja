using System;
using ScriptableObjects;
using UnityEngine;

namespace Controllers
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private HealthSettings settings;

        public event Action<int> OnLoadHealth;
        public event Action<int> OnHealing;
        public event Action<int> OnDamage;
        public event Action ZeroHealth;

        private int _healths;

        public void LoadHealth()
        {
            _healths = settings.MaxHeart;
            OnLoadHealth?.Invoke(settings.MaxHeart);
        }

        public void Healing()
        {
            if (_healths >= settings.MaxHeart)
            {
                return;
            }
            
            _healths += settings.Healing;
            OnHealing?.Invoke(settings.Healing);
        }

        public void Damage()
        {
            if (_healths == 0)
            {
                return;
            }
            
            _healths -= settings.Damage;
            OnDamage?.Invoke(settings.Damage);
            
            if (_healths == 0)
            { 
                ZeroHealth?.Invoke();
            }
        }
    }
}