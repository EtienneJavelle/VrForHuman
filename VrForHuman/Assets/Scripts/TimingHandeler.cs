using System.Collections;
using UnityEngine;

namespace CardiacMassage {
    public class TimingHandeler : MonoBehaviour {
        [SerializeField] private string[] TESTPointRanksName = new string[] { "Perfect", "Good", "Poor", "Mediocre", "Late" };
        [SerializeField] private int[] TESTPointRanks = new int[] { 1000, 800, 500, 100, 10 };
        [SerializeField] private int[] TESTPointRanksTimeOffset = new int[] { 50, 100, 150, 200, 250 };
        [SerializeField] private float beat;
        [SerializeField] private CardiacMassage cardiacMassage;
        [SerializeField] private AudioClip[] TESTBeatClips;
        private CardiacMassagePressureData lastPressure;
        [SerializeField] private AudioSource TESTaudio;
        private void Awake() {
            cardiacMassage ??= GetComponent<CardiacMassage>();
            cardiacMassage.OnPressureDone += pressure => CalcutaleTimingAccuracy(pressure);
            cardiacMassage.OnMassageStart += () => beatCoroutine = StartCoroutine(Beat());
            cardiacMassage.OnMassageStop += () => StopCoroutine(beatCoroutine);


            TESTaudio ??= GetComponent<AudioSource>();
            if(TESTaudio == null) {
                TESTaudio = gameObject.AddComponent<AudioSource>();
            }
            TESTaudio.playOnAwake = false;

        }

        private IEnumerator Beat() {
            while(true) {
                yield return new WaitForSeconds(beat / 1000f);
                TESTaudio.PlayOneShot(TESTBeatClips[Random.Range(0, TESTBeatClips.Length)]);
            }
        }


        private CardiacMassagePressureData CalcutaleTimingAccuracy(CardiacMassagePressureData pressure) {
            float time = (pressure.Time - lastPressure.Time) * 1000;
            print(time - beat);
            int rank = TESTPointRanksName.Length - 1;
            for(int i = 0; i < TESTPointRanksTimeOffset.Length; i++) {
                if(Mathf.Abs(time - beat) <= TESTPointRanksTimeOffset[i]) {
                    rank = i;
                    break;
                }
            }
            Debug.Log(TESTPointRanksName[rank]);
            lastPressure = pressure;
            return pressure;
        }

        private void Update() {

        }

        private Coroutine beatCoroutine;
    }
}