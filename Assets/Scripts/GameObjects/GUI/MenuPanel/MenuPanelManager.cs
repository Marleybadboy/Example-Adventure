using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCC.GameObjects.GUI
{
   /* The MenuPanelManager class manages the activation and deactivation of menu panels based on the
   current game state. */
    public class MenuPanelManager : MonoBehaviour
    {
        #region Fields
        public static MenuPanelManager instance;
        [Header("ManuPanel Structs")]
        public MenuPanelStruct[] _MenuPanelStructs;
        #endregion

        private void Awake()
        {
            instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            DisactiveAll();
        }

        public void ActivePanel(GameManager.GameState state) 
        { 
            foreach (var menu in _MenuPanelStructs) 
            { 
                if(menu.GameState == state) 
                { 
                    menu.Panel.SetActive(true);
                }
            }
        
        }

        public void DisactiveAll() 
        {
            foreach (var menu in _MenuPanelStructs)
            {
                menu.Panel?.SetActive(false);
            }
        }
   
    }
}
