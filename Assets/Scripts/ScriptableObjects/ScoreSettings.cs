using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ScoreSettings", menuName = "Score Settings", order = 0)]
    public class ScoreSettings : ScriptableObject
    {
        [SerializeField] private int scoreForSlicing;
        [SerializeField] private float intervalForCombo;
        [SerializeField] private int maxComboStage;

        public int ScoreForSlicing => scoreForSlicing;
        public float IntervalForCombo => intervalForCombo;
        public int MaxComboStage => maxComboStage;
    }
}