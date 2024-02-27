#if UNITY_EDITOR
using HCC.GameObjects.GUI;
using UnityEditor;

[CustomEditor(typeof(ButtonGameState))]
public class GameStateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        ButtonGameState t = (ButtonGameState)target;
    }
}
#endif
