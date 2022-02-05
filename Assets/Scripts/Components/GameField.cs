using Interfaces;
using UnityEngine;

namespace Components
{
    public class GameField : MonoBehaviour
    {
        [SerializeField] private Camera screenSpace;
        [Space]
        [SerializeField] private Vector2 point;
        [SerializeField] private Vector2 size;
        [Space]
        [SerializeField] private BaseCollider collisionSpace;

        private void Start()
        {
           // TODO: set bounds collisionSpace by self space
        }

        public Vector3 ViewportToGameField(Vector2 viewportPoint)
        {
            var worldPoint = screenSpace.ViewportToWorldPoint(viewportPoint);
            worldPoint.z = transform.position.z;
            return worldPoint;
        }
    }
}