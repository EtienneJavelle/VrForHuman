using System.Collections.Generic;
using UnityEngine;

namespace CardiacMassage {
    [CreateAssetMenu(fileName = "CardiacMassageSavingData", menuName = "VR For Human/CardiacMassageSavingData")]
    public class CardiacMassageSaving : ScriptableObject {
        public int totalScore { get; set; }

        public float maximumTimeReached { get; set; }

        public List<CardiacMassagePressureData> pushDatas { get; set; }

        public Rank[] timingRanks { get; set; }
        public Rank[] depthRanks { get; set; }
    }
}
