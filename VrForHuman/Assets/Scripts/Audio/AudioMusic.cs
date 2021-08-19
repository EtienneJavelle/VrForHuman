public class AudioMusic : AudioAmbiance {
    protected override void Start() {
        if(GameManager.Instance.IsArcadeMode) {
            base.Start();
        }
    }

}
