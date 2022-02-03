using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Components
{
    public static class Utils
    {
        public static Vector2 CurrentDisplay()
        {
            return new Vector2(Screen.width ,Screen.height);
        }
        
        public static Vector3 GetVelocity(float angle, float force)
        {
            // TODO: get velocity by angle and force
            return new Vector3();
        }

        public static Vector3 MiddleBetweenVector3(Vector3 pointA, Vector3 pointB)
        {
            return pointB - (pointB - pointA) * 0.5f;
        }

        public static Vector3 RandomRangeVector3(Vector3 minInclusive, Vector3 maxInclusive)
        {
            // TODO: Random.Range for Vector3 values
            return new Vector3();
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