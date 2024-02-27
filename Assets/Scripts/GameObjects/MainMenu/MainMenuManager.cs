
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace HCC.GameObjects.MainMenu
{
    /* The MainMenuManager class handles the main menu functionality, including starting a new game,
    loading a game, and quitting the application. */
    public class MainMenuManager : MonoBehaviour
    {
        public static MainMenuManager Instance;
        public enum MainMenuState {NEWGAME,LOAD,QUIT}
        #region Fields
        [SerializeField] private string _GameSceneAdress;

        private delegate void OnLoadGame();
        private OnLoadGame onLoadGame;
        #endregion

        #region Addressable
        private AsyncOperationHandle<SceneInstance> _LoadHandle;
        #endregion
        private void Awake()
        {
            Instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {

        }


       /// <summary>
       /// The function SetState takes a MainMenuState as input and performs different actions based on
       /// the value of the input.
       /// </summary>
       /// <param name="MainMenuState">MainMenuState is an enumeration that represents different states
       /// of the main menu. It could have values like NEWGAME, LOAD, and QUIT.</param>
        public void SetState(MainMenuState state) 
        { 
            switch (state) 
            { 
                case MainMenuState.NEWGAME:
                    StartGame();
                    break;
                case MainMenuState.LOAD:
                    LoadGame();
                    break; 
                case MainMenuState.QUIT:
                    Application.Quit();
                    break;
            
            }
        }

       /// <summary>
       /// The StartGame function loads a game scene asynchronously using Addressables and executes the
       /// StartGameHandle method when the loading is completed.
       /// </summary>
        private void StartGame() 
        { 
            _LoadHandle = Addressables.LoadSceneAsync(_GameSceneAdress, LoadSceneMode.Single);
            _LoadHandle.Completed += StartGameHandle;

        }

        private void StartGameHandle(AsyncOperationHandle<SceneInstance> obj)
        {
            Debug.Log(obj.Result);
            if(obj.Status == AsyncOperationStatus.Succeeded) 

            {
                if(onLoadGame != null) { onLoadGame.Invoke(); } else {  GameManager.Instance?.NewGame();}
               
            }
        }

      /// <summary>
      /// The LoadGame function registers the LoadGameAction method to be executed when the game is
      /// loaded and then starts the game.
      /// </summary>
        private void LoadGame() 
        {
            onLoadGame += LoadGameAction;
            StartGame();
        
        }
      /// <summary>
      /// The LoadGameAction function loads a game using the GameManager instance and removes the
      /// LoadGameAction from the onLoadGame event.
      /// </summary>
        private void LoadGameAction() 
        {
            GameManager.Instance?.LoadGame();
            onLoadGame -= LoadGameAction;

        }

    }
}
