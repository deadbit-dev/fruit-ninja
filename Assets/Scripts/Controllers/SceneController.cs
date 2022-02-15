using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class SceneController : MonoBehaviour
    {
        public static SceneController Instance;

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

        public void SwitchScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}