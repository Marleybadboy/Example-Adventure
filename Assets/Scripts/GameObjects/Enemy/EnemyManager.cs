using HCC.GameObjects.MainMenu;
using HCC.GameObjects.Player;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace HCC.GameObjects.Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance; 
        #region Fields
        public string _PathEnemyData;
        [SerializeField] public int _StartSpawnMultiplayer, _LevelMultiplayer;
        [SerializeField] private float _SpawnOffsetMin, _SpawnOffsetMax;
        [SerializeField] private EnemyData _ExampleEnemy;
        private List<EnemyData> m_EnemyData = new List<EnemyData>();
        public List<Enemy> _EnemiesList = new List<Enemy>();
        #endregion

        #region Properties
        private EnemySpawnPoint[] m_EnemySpawnPoints { get => FindObjectsOfType<EnemySpawnPoint>().ToArray(); }
        private float m_RandomPosX { get => UnityEngine.Random.Range(_SpawnOffsetMin, _SpawnOffsetMax); }
        private float m_RandomPosY { get => UnityEngine.Random.Range(_SpawnOffsetMin, _SpawnOffsetMax); }
        private GameManager.GameState m_GameState { get => GameManager.Instance._State;}

        #endregion
        private void Awake()
        {
            PlayerStats.EnemyLevelMultiplayer = _StartSpawnMultiplayer;
            Instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            SpawnAllEnemies(_StartSpawnMultiplayer);
            PlayerStats.KilledEnemies = 0;
            StaticEvents.onEnemyKill += StaticEvents_onEnemyKill;
            
        }
        private void OnDestroy()
        {
            StaticEvents.onEnemyKill -= StaticEvents_onEnemyKill;

        }
        private void Update()
        {
            
        }

        private void StaticEvents_onEnemyKill(object sender, int e)
        {
            CheckEnemies();
        }

        private async Task GetEnemyPrefab() 
        {
            await SaveEnemyData.LoadData();
            m_EnemyData = SaveEnemyData.EnemyDataBase;
        }
/// <summary>
/// The function "SpawnAllEnemies" spawns multiple enemies at random spawn points and assigns values to
/// them.
/// </summary>
/// <param name="multiplayer">The "multiplayer" parameter is an integer that determines how many enemies
/// should be spawned for each enemy spawn point.</param>

        public async void SpawnAllEnemies(int multiplayer)
        {
            await GetEnemyPrefab();
            GameManager.Instance?.FindPlayer();
            List<Enemy> enemies = new List<Enemy>();
            if (m_EnemySpawnPoints.Length > 0 && m_EnemyData.Count > 0)
            {
                foreach (EnemySpawnPoint enemySpawnPoint in m_EnemySpawnPoints)
                {
                    for (int i = 0; i < multiplayer; i++)
                    {
                        int randomindex = Random.Range(0, m_EnemyData.Count);
                        GameObject enemy = Instantiate(m_EnemyData[randomindex]._EnemyData.m_EnemyPrefab, new Vector2(enemySpawnPoint.m_Pos.x + m_RandomPosX, enemySpawnPoint.m_Pos.y + m_RandomPosY),
                            Quaternion.identity, enemySpawnPoint.transform);
                        Enemy spawnedenemy = enemy.GetComponent<Enemy>();
                        AssignEnemyValue(spawnedenemy, m_EnemyData[randomindex]);
                        enemies.Add(spawnedenemy);
                    }
                    
                    
                }
                

            }
            enemies.Add(SpawnExampleEnemy(m_EnemySpawnPoints[0]).GetComponent<Enemy>());
            _EnemiesList = enemies;

        }

       /// <summary>
       /// The CheckEnemies function checks the number of enemies in a list and updates the player's
       /// stats and game state if there are no more enemies.
       /// </summary>
        private void CheckEnemies() 
        {
            Enemy[] enemies = _EnemiesList.FindAll(enemy => enemy != null).ToArray();
            Debug.Log(enemies.Length);
            if(enemies.Length-1 < 1) 
            {
                PlayerStats.EnemyLevelMultiplayer += _LevelMultiplayer;
                GameManager.Instance?.SetState(GameManager.GameState.VICTORY);
            }
        }

        private void AdditionalCheck() 
        {
            List<Enemy> enemies = _EnemiesList.FindAll(enemy => enemy != null);
            if (enemies.Count < 1 && m_GameState != GameManager.GameState.VICTORY)
            {
                PlayerStats.EnemyLevelMultiplayer += _LevelMultiplayer;
                GameManager.Instance?.SetState(GameManager.GameState.VICTORY);
            }

        }

       /// <summary>
       /// The function AssignEnemyValue assigns the speed and damage values from an EnemyData object to
       /// an Enemy object.
       /// </summary>
       /// <param name="Enemy">The "Enemy" parameter is an instance of the "Enemy" class, which
       /// represents an enemy in the game.</param>
       /// <param name="EnemyData">EnemyData is a class that contains data about an enemy, such as its
       /// speed and damage. It has a property called _EnemyData, which is an instance of another class
       /// that holds the actual values for speed and damage.</param>
        private void AssignEnemyValue(Enemy enemy, EnemyData enemyData) 
        { 
            enemy._Speed = enemyData._EnemyData.Speed;
            enemy._Damage = (int)enemyData._EnemyData.Damage;
        }

        /// <summary>
        /// The function spawns an example enemy at a given position and assigns values to its
        /// properties.
        /// </summary>
        /// <param name="EnemySpawnPoint">EnemySpawnPoint is a class or script that represents a spawn
        /// point for enemies in the game. It likely contains information such as the position of the
        /// spawn point and any other relevant data for spawning enemies.</param>
        /// <returns>
        /// The method is returning a GameObject.
        /// </returns>
        private GameObject SpawnExampleEnemy(EnemySpawnPoint pos) 
        { 
            GameObject obj = Instantiate(_ExampleEnemy._EnemyData.m_EnemyPrefab, 
                new Vector2(pos.m_Pos.x + m_RandomPosX, pos.m_Pos.y + m_RandomPosY), Quaternion.identity, pos.transform);

            Enemy enemy = obj.GetComponent<Enemy>();
            AssignEnemyValue(enemy, _ExampleEnemy);
            return obj;
        }
      
    }
}

