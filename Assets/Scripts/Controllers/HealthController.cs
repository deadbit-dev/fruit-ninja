using System;
using ScriptableObjects;
using UnityEngine;

namespace Controllers
{
    public class HealthController : MonoBehaviour
    {
        public static HealthController Instance;
        
        [SerializeField] private HealthSettings settings;

        public event Action ZeroHeart;

        private int hearts;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            hearts = settings.CountHeart;
            UIController.Instance.SetHeart(hearts);
        }

        public void Damage()
        {
            if (hearts == 0)
            { 
                ZeroHeart?.Invoke();
                return;
            }
            
            hearts -= settings.DamageHeartCount;
            UIController.Instance.Damage(settings.DamageHeartCount);
        }
    }
}