using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "HealthSettings", menuName = "Health Settings", order = 0)]
    public class HealthSettings : ScriptableObject
    {
        [SerializeField] private int countHeart;
        [SerializeField] private int damageHeartCount;
        
        public int CountHeart => countHeart;
        public int DamageHeartCount => damageHeartCount;
    }
}