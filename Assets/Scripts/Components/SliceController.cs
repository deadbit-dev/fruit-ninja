using UnityEngine;

namespace Components
{
    public class SliceController : MonoBehaviour
    {
        public static SliceController Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void SliceUnit(GameObject unit)
        {
            
        }
    }
}