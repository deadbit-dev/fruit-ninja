using System.Collections.Generic;
using System.Linq;

namespace Components.Utils
{
    public static class Random
    {
        public static int RandomRangeWeight(IReadOnlyList<float> probes)
        {
            var total = probes.Sum();
            var randomPoint = UnityEngine.Random.value * total;
            
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