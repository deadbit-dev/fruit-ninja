using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "HealthSettings", menuName = "Health Settings", order = 0)]
    public class HealthSettings : ScriptableObject
    {
        [SerializeField] private int maxHeart;
        [SerializeField] private int healing;
        [SerializeField] private int damage;
        
        public int MaxHeart => maxHeart;
        public int Healing => healing;
        public int Damage => damage;
    }
}