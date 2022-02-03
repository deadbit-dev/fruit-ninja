using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Components
{
    public static class Utils
    {
        public static Vector3 ScreenToWorldPoint(Vector2 point, Camera screen)
        {
            return screen.ScreenToWorldPoint(point * new Vector2(Screen.width, Screen.height));
        }
        
        public static Vector3 MiddleBetweenVector3(Vector3 pointA, Vector3 pointB)
        {
            return pointB - (pointB - pointA) * 0.5f;
        }

        public static Vector2 RandomRangeVector2(Vector2 minInclusive, Vector2 maxInclusive)
        {
            // TODO: Random.Range for Vector3 values
            return new Vector2();
        }
        
        public static int RandomRangeWeight(IReadOnlyList<float> probes)
        {
            var total = probes.Sum();
            var randomPoint = Random.value * total;
            
            for (var i = 0; i < probes.Count; i++)
            {
                if (randomPoint < probes[i])
                {
                    return i;
                }
                
                randomPoint -= probes[i];
            }

            return probes.Count - 1;
        }
    }
}