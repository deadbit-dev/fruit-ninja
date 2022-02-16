using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class SceneController : MonoBehaviour
    {
        public static SceneController Instance;

        private void Awake()
        {
            Instance = this;
        }

        public static void SwitchScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}