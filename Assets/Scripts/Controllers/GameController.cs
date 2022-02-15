using ScriptableObjects;
using UnityEngine;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;
        
        [SerializeField] private GameSettings settings;

        private void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}