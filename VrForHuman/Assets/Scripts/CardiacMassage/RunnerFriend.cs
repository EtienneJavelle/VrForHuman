using System.Collections;
using UnityEngine;

public class RunnerFriend : MonoBehaviour {
    [SerializeField] private Runner runner;

    [SerializeField] private float timeBeforeBringDefibrilator;

    [SerializeField] private Etienne.Path toDefibrilatorPath, returnDefibrilatorPath;

    public void SetCurrentPathToDefibrilatorPath() {
        runner.SetCurrentPath(toDefibrilatorPath);
        runner.DefibrilatorPath();
    }

    public void SetCurrentPathToReturnDefibrilatorPath() {
        runner.SetCurrentPath(returnDefibrilatorPath);
        runner.ReturnDefibrilatorPath();
    }

    public IEnumerator TimerBeforeBringDefibrilator() {
        yield return new WaitForSeconds(timeBeforeBringDefibrilator);

        runner.SetActiveVisual(true);
        SetCurrentPathToReturnDefibrilatorPath();
    }
}
