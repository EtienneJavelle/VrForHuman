public class TeacherCtrl : Etienne.Singleton<TeacherCtrl> {

    private void Start() {
        TestDebug.Instance.ResetCallRescueSteps();
    }

    public void CallRescueStep01Button() {
        TestDebug.Instance.CallRescueStep01Completed();
    }

    public void CallRescueStep02Button() {
        TestDebug.Instance.CallRescueStep02Completed();
    }

    public void CallRescueStep03Button() {
        TestDebug.Instance.CallRescueStep03Completed();
    }
}
