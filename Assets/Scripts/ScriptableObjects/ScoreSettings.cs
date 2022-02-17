using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ScoreSettings", menuName = "Score Settings", order = 0)]
    public class ScoreSettings : ScriptableObject
    {
        [SerializeField] private int scoreForSlicing;

        public int ScoreForSlicing => scoreForSlicing;
    }
}