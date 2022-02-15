using System;
using ScriptableObjects;
using UnityEngine;

namespace Controllers
{
    public class HealthController : MonoBehaviour
    {
        public static HealthController Instance;
        
        [SerializeField] private HealthSettings settings;
        [SerializeField] private UIController uiController;

        public event Action ZeroHeart;

        private int hearts;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            hearts = settings.CountHeart;
        }

        public void Damage()
        {
            hearts -= settings.DamageHeartCount;

            if (hearts == 0)
            {
               ZeroHeart?.Invoke(); 
            }
        }
    }
}