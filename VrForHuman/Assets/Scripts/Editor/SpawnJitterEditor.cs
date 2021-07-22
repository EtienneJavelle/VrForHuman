using EtienneEditor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnJitter))]
[CanEditMultipleObjects]
public class SpawnJitterEditor : Editor<SpawnJitter> {
    private void OnSceneGUI() {
        Handles.color = Color.white;
        Handles.DrawSolidArc(Target.transform.position, Vector3.back, Vector3.up, 360, Target.DiaplayRange);
        Vector2 randomPosition = Random.insideUnitCircle * Target.DiaplayRange;
        Handles.color = Color.red;
        Handles.DrawWireCube(Target.transform.position + (Vector3)randomPosition, Vector3.one * Target.DiaplayRange / 4);
    }
}