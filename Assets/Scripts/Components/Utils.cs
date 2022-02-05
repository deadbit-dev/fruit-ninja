using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Components
{
    public static class Utils
    {
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