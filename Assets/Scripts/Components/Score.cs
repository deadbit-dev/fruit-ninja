using UnityEngine;

namespace Components
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private int count;

        public int Count => count;
    }
}