#if UNITY_EDITOR

using UnityEditor;


namespace HCC.GameObjects.MainMenu
{
    [CustomEditor(typeof(ButtonMainMenuState))]
    public class MainMenuEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ButtonMainMenuState t = (ButtonMainMenuState)target;
        }
    }
}
#endif
