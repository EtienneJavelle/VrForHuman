public class TeacherCtrl : Etienne.Singleton<TeacherCtrl> {
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
