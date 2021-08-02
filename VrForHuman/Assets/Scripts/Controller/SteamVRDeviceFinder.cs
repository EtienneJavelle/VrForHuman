using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Valve.VR;

public class SteamVRDeviceFinder : MonoBehaviour {
    [SerializeField] private InputAction findNext;
    [SerializeField] private bool ignoreHMD = true, ignorebases = true;

    private Coroutine coroutine;

    private void Start() {
        SteamVR_TrackedObject obj = GetComponent<SteamVR_TrackedObject>();
        if(!obj.isValid) {
            coroutine = StartCoroutine(FindDeviceCoroutine(obj));
        }
        findNext.performed += _ => FindDevice(obj);
    }

    private void FindDevice(SteamVR_TrackedObject obj) {
        if(coroutine != null) {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(FindDeviceCoroutine(obj));
    }

    public IEnumerator FindDeviceCoroutine(SteamVR_TrackedObject obj) {
        obj.index--;
        if(obj.index < SteamVR_TrackedObject.EIndex.Hmd) {
            obj.index = SteamVR_TrackedObject.EIndex.Device16;
        }
        if(ignorebases && (obj.index == SteamVR_TrackedObject.EIndex.Device1 || obj.index == SteamVR_TrackedObject.EIndex.Device2)) {
            obj.index = SteamVR_TrackedObject.EIndex.Hmd;
        }
        if(ignoreHMD && obj.index == SteamVR_TrackedObject.EIndex.Hmd) {
            obj.index = SteamVR_TrackedObject.EIndex.Device16;
        }

        yield return new WaitForEndOfFrame();
        if(!obj.isValid) {
            FindDevice(obj);
        }

    }

    private void OnEnable() {
        findNext.Enable();
    }

    private void OnDisable() {
        findNext.Disable();
    }
}
