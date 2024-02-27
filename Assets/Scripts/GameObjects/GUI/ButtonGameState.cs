
using UnityEditor;
using UnityEngine.UI;

namespace HCC.GameObjects.GUI
{

    public class ButtonGameState : Button
    {
        public GameManager.GameState _GameState;

        public void InvokeGameState() 
        {
            GameManager.Instance?.SetState(_GameState);
        }
    }
}
