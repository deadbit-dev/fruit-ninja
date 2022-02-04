using TMPro;
using UnityEngine;

namespace Components
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private Camera screenSpace;
        [Space] 
        //[Header("Corners")]
        //[SerializeField] private Vector2 bottomLeft;
        //[SerializeField] private Vector2 topRight;
        [SerializeField] private Bounds bounds;

        public Vector3 UnitPointToGameFieldSpace(Vector2 unitPoint)
        {
            // TODO: fix this method, get point in game field space with zero value by z axis
            var gameFieldSpacePoint = screenSpace.ScreenToWorldPoint(unitPoint * new Vector2(Screen.width, Screen.height));
            gameFieldSpacePoint.z = 0;
            return gameFieldSpacePoint;
        }
    }
}