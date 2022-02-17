using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class SceneController : MonoBehaviour
    {
        private static SceneController _instance;

        private void Awake()
        {
            _instance = this;
        }

        public static void SwitchScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}