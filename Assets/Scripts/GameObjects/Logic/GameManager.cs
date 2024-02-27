using DG.Tweening;
using HCC.GameObjects;
using HCC.GameObjects.Enemy;
using HCC.GameObjects.GUI;
using HCC.GameObjects.Player;
using HCC.GameObjects.Save;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

/* The GameManager class manages the game state, player data, scene loading, and saving/loading game
progress. */
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum GameState {RESTART,PAUSE,VICTORY,FAILURE,QUITGAME,MAINMENU,NEXTLEVEL,CONTINUE,SAVE,LOAD}

    #region Fields
    public GameState _State = GameState.RESTART;
    
    [Header("Player")]
    [SerializeField] private GameObject _PlayerObject;
    [SerializeReference] public Player _Player;
    public int _PlayerHealth;
    public int _PlayerAmmo;
   
    [Header("Scene Adress")]
    [SerializeField] private string[] _SceneAddress;
   
    [Header("Save Game")]
    public string _FolderPath;
    #endregion

    #region Addressabels
    private AsyncOperationHandle<SceneInstance> _LoadHandle = new AsyncOperationHandle<SceneInstance>();
    private SceneInstance _SubsceneInstance = new SceneInstance();
    private string _MainMenuSceneAdress = "Assets/Scenes/MainMenuScene.unity";
    
    public delegate void SceneAction();
    private SceneAction _SceneAction;
    public Action FindPlayer;
    #endregion

    #region Properties
    public int m_TimeScale { set { Time.timeScale = value; } }
    private int m_RandomIndex { get => UnityEngine.Random.Range(0, _SceneAddress.Length); }
    private int m_NextLevelMultiplayer { get => PlayerStats.EnemyLevelMultiplayer; }
    private MenuPanelManager m_MenuPanelManager { get => MenuPanelManager.instance; }
    #endregion
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        FindPlayer += () => { if (_Player == null) { _Player = FindObjectOfType<Player>(); } };
        
    }

/// <summary>
/// The SetState function is used to handle different game states and perform corresponding actions such
/// as restarting the game, pausing the game, saving the game, etc.
/// </summary>
/// <param name="GameState">GameState is an enum that represents different states of the game. The
/// possible values are CONTINUE, RESTART, PAUSE, VICTORY, FAILURE, NEXTLEVEL, MAINMENU, SAVE, LOAD, and
/// QUITGAME.</param>
    public async void SetState(GameState state) 
    { 
        switch (state) 
        { 
            case GameState.CONTINUE:
                m_TimeScale = 1;
                m_MenuPanelManager.DisactiveAll();
                break;
            case GameState.RESTART:
                DOTween.KillAll();
                m_TimeScale = 1;
                RestartGame();
                m_MenuPanelManager.DisactiveAll();
                break;
            case GameState.PAUSE:
                DOTween.KillAll();
                m_TimeScale = 0;
                m_MenuPanelManager.ActivePanel(state);
                break;
            case GameState.VICTORY:
                DOTween.KillAll();
                m_TimeScale = 0;
                m_MenuPanelManager.ActivePanel(state);
                break;
            case GameState.FAILURE:
                DOTween.KillAll();
                m_TimeScale = 0;
                m_MenuPanelManager.ActivePanel(state);
                break;
            case GameState.NEXTLEVEL:
                DOTween.KillAll();
                m_TimeScale = 1;
                NextLevel();
                _SceneAction += StartGame;
                m_MenuPanelManager.DisactiveAll();
                break;
            case GameState.MAINMENU:
                m_TimeScale = 1;
                MainMenu();
                break;
            case GameState.SAVE:
                await new SaveGame { FolderName = _FolderPath }.Save();
                m_TimeScale = 1;
                m_MenuPanelManager.DisactiveAll();
                break;
            case GameState.LOAD:
                m_MenuPanelManager.DisactiveAll();
                LoadGame();
                break;
            case GameState.QUITGAME:
                 Application.Quit();
                break;
            default:
            break;
        }
        _State = state;
    
    }
/// <summary>
/// The NewGame function initializes various game variables and sets up the player's starting stats and
/// GUI.
/// </summary>
    public void NewGame() 
    {
        _SceneAction += StartGame;
        LoadScene(0);
        PlayerStats.CurrentLevel = 1;
        PlayerStats.PlayerHealth = _PlayerHealth;
        PlayerStats.EnemyLevelMultiplayer = EnemyManager.Instance._StartSpawnMultiplayer;
        PlayerGUIManager.instance.m_KillCounter = 0;
        PlayerStats.KilledEnemies = 0;
        PlayerStats.LevelMapIndex = 0;
        PlayerStats.AvailableAmmo = _PlayerAmmo;
        PlayerGUIManager.instance?.InitializeAmmo(_PlayerAmmo);
        PlayerGUIManager.instance?.CalulateHealth();
    }
/// <summary>
/// The function "LoadScene" loads a scene asynchronously using Addressables, updates the player's level
/// map index, and invokes a specified action upon completion.
/// </summary>
/// <param name="sceneIndex">The index of the scene to be loaded.</param>
    public void LoadScene(int sceneIndex) 
    {
        _LoadHandle = Addressables.LoadSceneAsync(_SceneAddress[sceneIndex], LoadSceneMode.Additive);
        PlayerStats.LevelMapIndex = sceneIndex;
        _LoadHandle.Completed += LoadHandle_SceneLoad;
        _LoadHandle.Completed += handle => { _SceneAction.Invoke(); };
    }

/// <summary>
/// The function loads a scene asynchronously and sets the time scale to 1 if the loading is successful.
/// </summary>
/// <param name="obj">AsyncOperationHandle<SceneInstance> obj is an asynchronous operation handle that
/// represents the loading of a scene. It contains information about the status and result of the
/// loading operation.</param>
    private void LoadHandle_SceneLoad(AsyncOperationHandle<SceneInstance> obj)
    {
        if(obj.Status == AsyncOperationStatus.Succeeded) 
        {
            _SubsceneInstance = _LoadHandle.Result;
            m_TimeScale = 1;
        }
    }

   /// <summary>
   /// The StartGame function destroys the player object if it exists, creates a new player object,
   /// shows the level info panel, finds the player object if it doesn't exist, spawns all enemies, and
   /// removes the StartGame function from the scene action.
   /// </summary>
    public  void StartGame() 
    {
        if(_Player != null) {Destroy(_Player.gameObject); }
        
        CreatePlayer(out GameObject player);
        _Player = player.GetComponentInChildren<Player>();
        InfoPanelManager.instance?.ShowLevel();
        
        if( _Player == null ) 
        { 
            _Player = FindObjectOfType<Player>();
        }
        
        EnemyManager.Instance?.SpawnAllEnemies(m_NextLevelMultiplayer);
        _SceneAction -= StartGame;
    }
/// <summary>
/// The LoadGame function unloads a scene if it is already loaded, otherwise it initializes the loading
/// of a new scene.
/// </summary>
    public void LoadGame() 
    {
         
        if (_SubsceneInstance.Scene.isLoaded)
        {
            AsyncOperationHandle<SceneInstance>  handle = Addressables.UnloadSceneAsync(_SubsceneInstance, true);
            handle.Completed += UnloadScene_Handle;
        }
        else
        {
            InitializeLoadScene();
        } 
    }

    private void UnloadScene_Handle(AsyncOperationHandle<SceneInstance> obj)
    {
        if( obj.Status == AsyncOperationStatus.Succeeded) 
        {
            InitializeLoadScene();

        }
    }

   /// <summary>
   /// The function initializes the loading of a scene and attempts to load a saved game, then calls the
   /// LoadGameStatus function and loads a specific scene.
   /// </summary>
    private async void InitializeLoadScene() 
    {

        try
        {
            await new SaveGame { FolderName = _FolderPath }.Load();
        }
        catch (Exception) { }
        _SceneAction += LoadGameStatus;
        LoadScene(PlayerStats.LevelMapIndex);

    }
/// <summary>
/// The LoadGameStatus function starts the game, calculates the player's health, initializes the
/// player's ammo, triggers the OnLoadGame event, and removes the LoadGameStatus function from the
/// _SceneAction delegate.
/// </summary>

    private void LoadGameStatus() 
    { 
        StartGame();
        PlayerGUIManager.instance?.CalulateHealth();
        PlayerGUIManager.instance?.InitializeAmmo(PlayerStats.AvailableAmmo);
        StaticEvents.OnLoadGame(this);
        _SceneAction -= LoadGameStatus;
    }
    private void CreatePlayer(out GameObject player) 
    {
        player = Instantiate(_PlayerObject);
        SceneManager.MoveGameObjectToScene(player, _SubsceneInstance.Scene);
    }

  /// <summary>
  /// The RestartGame function unloads a scene asynchronously, then creates a new scene instance and
  /// starts a new game.
  /// </summary>
    private void RestartGame() 
    {
        
        AsyncOperationHandle<SceneInstance> handle = Addressables.UnloadSceneAsync(_SubsceneInstance, true);
        handle.Completed += (o) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _SubsceneInstance = new SceneInstance();
                NewGame();
            }
        };

    }
    #region NextLevel
  /// <summary>
  /// The NextLevel function unloads a scene asynchronously and calls the Handle_NextLevel function when
  /// the operation is completed.
  /// </summary>
    private void NextLevel() 
    {
        Debug.Log(_SubsceneInstance.Scene.name);
        if (_SubsceneInstance.Scene.isLoaded)
        {
            AsyncOperationHandle<SceneInstance> handle = Addressables.UnloadSceneAsync(_SubsceneInstance, true);
            handle.Completed += Handle_NextLevel;
        }
        
    }

    private void Handle_NextLevel(AsyncOperationHandle<SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            PlayerStats.CurrentLevel++;
            LoadScene(m_RandomIndex);
        }
    }
    #endregion

    public void ShowPause() 
    {
        SetState(GameState.PAUSE);
    }

    /// <summary>
    /// The MainMenu function loads the main menu scene asynchronously using Addressables.
    /// </summary>
    public void MainMenu() 
    {
        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(_MainMenuSceneAdress, LoadSceneMode.Single);
        handle.Completed += Handle_MainMenuCompleted;

    }

    private void Handle_MainMenuCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
       if(obj.Status == AsyncOperationStatus.Failed) 
       {
            Debug.LogError($"Scene load failed {_MainMenuSceneAdress}");     
       }
    }
}
