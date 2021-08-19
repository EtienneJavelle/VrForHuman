using UnityEngine;

namespace CardiacMassage {
    [System.Serializable]
    public struct TimerStep {
        public int RythmTimeReached => rythmTimeReached;
        public float MultiplierScore => multiplierScore;

        [SerializeField] private int rythmTimeReached;
        [SerializeField] private float multiplierScore;
    }
}
