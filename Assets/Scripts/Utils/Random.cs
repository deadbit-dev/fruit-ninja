using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class Random
    {
        public static int RandomRangeWeight(IReadOnlyList<float> probes)
        {
            var randomPoint = UnityEngine.Random.value * probes.Sum();
            
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