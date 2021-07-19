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

    public void ClassicMode() {
        GameManager.Instance.IsArcadeMode = false;
        SceneLoader.instance.ChangeScene(Scenes.CardiacMassage);
    }

    public void ArcadeMode() {
        GameManager.Instance.IsArcadeMode = true;
        SceneLoader.instance.ChangeScene(Scenes.CardiacMassage);
    }

    public void QuitGame() {
        Application.Quit();
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
        Debug.Log("press");
        data.pointerPressRaycast = data.pointerCurrentRaycast;

        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(currentObject, data, ExecuteEvents.pointerDownHandler);

        if(newPointerPress == null) {
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);
        }

        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = currentObject;
    }

    private void ProcessRelease(PointerEventData data) {
        Debug.Log("releaes");

        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        GameObject pointerUpHandeler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

        if(data.pointerPress == pointerUpHandeler) {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
        }

        eventSystem.SetSelectedGameObject(null);

        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;
    }

}
