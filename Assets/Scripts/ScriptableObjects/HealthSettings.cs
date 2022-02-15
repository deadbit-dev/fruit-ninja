using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "HealthSettings", menuName = "Health Settings", order = 0)]
    public class HealthSettings : ScriptableObject
    {
        [SerializeField] private int countHeart;
        [SerializeField] private int damageHeart;
        
        public int CountHeart => countHeart;
        public int DamageHeart => damageHeart;
    }
}