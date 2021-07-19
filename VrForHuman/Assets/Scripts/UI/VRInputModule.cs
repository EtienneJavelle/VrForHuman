using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInputModule : BaseInputModule {
    public PointerEventData GetData => data;

    [SerializeField] private new Camera camera;
    [SerializeField] private SteamVR_Input_Sources targetSource;
    [SerializeField] private SteamVR_Action_Boolean clickAction;
    private GameObject currentObject = null;
    private PointerEventData data = null;

    protected override void Awake() {
        base.Awake();

        data = new PointerEventData(eventSystem);
    }

    public override void Process() {
        //reset data, set
        data.Reset();
        data.position = new Vector2(camera.pixelWidth / 2f, camera.pixelHeight / 2f);

        //raycast
        eventSystem.RaycastAll(data, m_RaycastResultCache);
        data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        currentObject = data.pointerCurrentRaycast.gameObject;

        //clear raycast
        m_RaycastResultCache.Clear();

        //hover state
        HandlePointerExitAndEnter(data, currentObject);

        //press input
        if(clickAction.GetStateDown(targetSource)) {
            ProcessPress(data);
        }

        //release input
        if(clickAction.GetStateUp(targetSource)) {
            ProcessRelease(data);
        }
    }

    private void ProcessPress(PointerEventData data) {

    }

    private void ProcessRelease(PointerEventData data) {

    }

}
