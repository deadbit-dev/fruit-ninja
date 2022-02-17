using UnityEngine;

namespace Controllers
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private ScoreController scoreController;
        
        private void Start()
        {
            scoreController.LoadScore();
        }
    }
}