using UnityEngine;

namespace Controllers
{
    public class StartController : MonoBehaviour
    {
        private static StartController _instance;

        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            ScoreController.Instance.LoadScore();
        }
    }
}