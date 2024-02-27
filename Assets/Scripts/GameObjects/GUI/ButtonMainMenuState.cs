
using UnityEngine.UI;

namespace HCC.GameObjects.MainMenu
{
    public class ButtonMainMenuState : Button
    {
        public MainMenuManager.MainMenuState _MainMenuState;

        public void InvokeMenuState()
        {
            MainMenuManager.Instance?.SetState(_MainMenuState);
        }
    }
}
