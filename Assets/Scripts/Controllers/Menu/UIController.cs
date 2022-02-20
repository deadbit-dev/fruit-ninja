using UnityEngine;
using UnityEngine.UI;

namespace Controllers.Menu
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Text bestScoreCount;

        private void Start()
        {
            bestScoreCount.text = ScoreController.GetBestScore().ToString();
        }
    }
}