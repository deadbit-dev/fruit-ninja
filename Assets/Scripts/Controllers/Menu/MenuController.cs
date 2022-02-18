using UnityEngine;

namespace Controllers.Menu
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