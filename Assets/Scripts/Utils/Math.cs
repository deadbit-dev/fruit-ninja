using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class Math
    {
        public static int RandomRangeWeight(IReadOnlyList<float> weights)
        {
            return ClampRangeWeight(weights, UnityEngine.Random.value);
        }
        
        public static int ClampRangeWeight(IReadOnlyList<float> weights, float value)
        {
            var point = value * weights.Sum();
            
            for (var i = 0; i < weights.Count; i++)
            {
                if (point < weights[i])
                {
                    return i;
                }
                
                point -= weights[i];
            }

            return weights.Count - 1;
        }
    }
}