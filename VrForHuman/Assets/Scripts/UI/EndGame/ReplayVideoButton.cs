using System.Collections;
using UnityEngine;

public class ReplayVideoButton : ButtonUI {
    #region Properties

    public int videoIndex { get; set; }

    #endregion

    public IEnumerator VideoPlayerTimerActivation() {
        yield return new WaitForSeconds(1f);

        GetComponent<UnityEngine.Video.VideoPlayer>().enabled = false;
    }
}
