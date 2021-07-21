using System.Collections.Generic;

namespace CardiacMassage {
    [System.Serializable]
    public struct CardiacMassageSavingData {
        public int totalScore { get; set; }

        public float maximumTimeReached { get; set; }

        public List<CardiacMassagePressureData> pushDatas { get; set; }

        public Rank[] timingRanks { get; set; }
        public Rank[] depthRanks { get; set; }
    }
}
