using HCC.GameObjects.Enemy;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HCC.GameObjects.MainMenu
{
  /* The EnemyEditor class is responsible for creating and managing enemy objects in a game, allowing
  the user to customize their stats and attacks. */
    public class EnemyEditor : MonoBehaviour
    {
        public GameObject enemy;
        #region Fields
        [Header("Enemy Icon Template")]
        [SerializeField] private GameObject _Template;

        [Header("Enemy Spawn Container")]
        [SerializeField] private RectTransform  _CreatedContainer;
        [SerializeField] private RectTransform _ChooseContainer;

        [Header("Buttons")]
        [SerializeField] private Button _CreateEnemy;
        [SerializeField] private Button _DeleteEnemy;

        [Header("Stats")]
        [SerializeField] private Slider  _EnemyDamage;
        [SerializeField] private Slider _EnemySpeed;

        [Header("Toggle Groups")]
        [SerializeField] private ToggleGroup _GroupCreated;
        [SerializeField] private ToggleGroup _GroupeChoose;

        [Header("Data Paths")]
        [SerializeField] private string _ChoosenDataPath;
        [SerializeField] private string _CreatedDataPath;

        [Header("Enemy Visual Overview")]
        [SerializeField] private Image _EnemyOverview;

        [Header("Enemy Attack")]
        [SerializeField] private TMP_Dropdown _DropdownAttack;
        [SerializeReference,SubclassSelector] public IEnemyAttack[] _EnemyAttack;

        [Header("Enemy Name")]
        [SerializeField] private TMP_InputField _EnemyName;

        private List<GameObject> _SpawnedObjectCreated = new List<GameObject>();
        private List<GameObject> _SpawnedObjectChosen = new List<GameObject>();
        private int _EnemyGameObjectIndex;
        #endregion

        #region Properties
        private GameObject[] m_ChooseEnemyData { get => Resources.LoadAll<GameObject>(_ChoosenDataPath); }
        private List<EnemyData> m_CreateEnemyData { get => SaveEnemyData.EnemyDataBase; }
        private Sprite m_EnemyOverviewSprite { get => _EnemyOverview.sprite; set { _EnemyOverview.sprite = value; } }
        private bool m_EnemyOverviewActiv { set {_EnemyOverview.enabled = value; } }
        private float m_EnemyDamageValue { get => _EnemyDamage.value; }
        private float m_EnemySpeedValue { get => _EnemySpeed.value; }
        private string m_EnemyName { get => _EnemyName.text;}
        private IEnemyAttack m_EnemyAttack { get => _EnemyAttack[_DropdownAttack.value];}

        private bool m_CreateButtonIni { set { _CreateEnemy.interactable = value;} }

        #endregion
        // Start is called before the first frame update
       async void Start() 
       {
            await StartStateAsync();
       }

        #region Methods

      /// <summary>
      /// The StartStateAsync function loads enemy data, spawns containers, adds listeners, and sets the
      /// enemy overview activation to false.
      /// </summary>
        private async Task StartStateAsync() 
        {
            await SaveEnemyData.LoadData();
            SpawnAllContainers();
            AddListeners();
            m_EnemyOverviewActiv = false;
            
        }
       /// <summary>
       /// The function spawns and creates a container of game objects based on a list of enemy data.
       /// </summary>
        private void SpawnCreateContainerData() 
        { 
            List<GameObject> list = new List<GameObject>();
            for(int i =0;  i < m_CreateEnemyData.Count; i++) 
            {
                GameObject ob = Instantiate(_Template, _CreatedContainer);
                UICreateObject(ref ob, m_CreateEnemyData[i], i);
                list.Add(ob);

            }
            _SpawnedObjectCreated = list;
        }

/// <summary>
/// The UICreateObject function is used to create UI objects and set their properties based on the
/// provided data.
/// </summary>
/// <param name="GameObject">The "ob" parameter is a reference to a GameObject that will be modified in
/// the method.</param>
/// <param name="EnemyData">EnemyData is a custom data structure that contains information about an
/// enemy, such as its sprite and other properties.</param>
/// <param name="index">The "index" parameter is an integer that represents the index of the object in a
/// collection or list. It is used to keep track of the position of the object in the collection and is
/// passed as an argument to other methods or functions.</param>
        private void UICreateObject(ref GameObject ob, EnemyData item, int index) 
        {
            ob.transform.GetChild(0).GetComponent<Image>().sprite = item._EnemyData.m_EnemySprite;
            Toggle toggle = ob.GetComponent<Toggle>();
            toggle.group = _GroupCreated;
            toggle.onValueChanged.AddListener(value => { DeleteButton(index); });
        }

        private void UICreateObject(ref GameObject ob, GameObject item, int index)
        {
            ob.transform.GetChild(0).GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
            Toggle toggle = ob.GetComponent<Toggle>();
            toggle.group = _GroupeChoose;
            toggle.onValueChanged.AddListener(value => { ShowEnemy(index); });
        }

        private void SpawnChooseContainerData() 
        {
            List<GameObject> list = new List<GameObject>();
            for (int i = 0; i < m_ChooseEnemyData.Length; i++)
            {
                GameObject ob = Instantiate(_Template, _ChooseContainer);
                UICreateObject(ref ob, m_ChooseEnemyData[i], i);
                list.Add(ob);

            }
            _SpawnedObjectChosen = list;

        }

        private void SpawnAllContainers() 
        { 
            SpawnChooseContainerData();
            SpawnCreateContainerData();
        }

     /// <summary>
     /// The ShowEnemy function sets the enemy overview sprite, updates the enemy game object index,
     /// checks if the create button should be enabled, and sets the enemy overview activity to true.
     /// </summary>
     /// <param name="index">The index parameter is an integer that represents the index of the enemy
     /// object in the _SpawnedObjectChosen array.</param>
        public void ShowEnemy(int index) 
        {
            m_EnemyOverviewSprite = _SpawnedObjectChosen[index].transform.GetChild(0).GetComponent<Image>().sprite;
            _EnemyGameObjectIndex = index;
            CheckCreateButton();
            m_EnemyOverviewActiv = true;
        }

        #region Create Enemy 
        /// <summary>
        /// The CreateEnemy function creates an enemy object, assigns values to it, saves the enemy
        /// data, destroys the load, and logs the count of created enemies.
        /// </summary>
        private async void CreateEnemy() 
        {
            if (EnemyCanByCreate()) 
            { 
                EnemyData enemyData = CreateEnemyData();
                Enemy.Enemy myenemy = enemyData._EnemyData.m_EnemyPrefab.GetComponent<Enemy.Enemy>();
                AssignEnemyValues(ref myenemy, enemyData);
                SaveEnemyData.EnemyDataBase.Add(enemyData);
                await SaveEnemyData.SaveData();
                DestroyLoad();
                Debug.Log(m_CreateEnemyData.Count);
            
            }
        
        }

      /// <summary>
      /// The function AssignEnemyValues assigns values from an EnemyData object to an Enemy object.
      /// </summary>
      /// <param name="enemy">The "enemy" parameter is a reference to an instance of the "Enemy"
      /// class.</param>
      /// <param name="EnemyData">A class that contains data about an enemy, such as its damage, speed,
      /// and attack type.</param>
        private void AssignEnemyValues(ref Enemy.Enemy enemy, EnemyData data) 
        {
            enemy._Damage = (int)data._EnemyData.Damage;
            enemy._Speed = data._EnemyData.Speed;
            enemy._Attack = data._EnemyAttack;

        }
     /// <summary>
     /// The function CreateEnemyData creates an instance of the EnemyData class and initializes its
     /// properties using the GetEnemyStruct function and the m_EnemyAttack variable.
     /// </summary>
     /// <returns>
     /// The method is returning an instance of the EnemyData class.
     /// </returns>
        private EnemyData CreateEnemyData() 
        { 
            EnemyData enemyData = new EnemyData 
            { 
                _EnemyData = GetEnemyStruct(),
                _EnemyAttack = m_EnemyAttack
            
            };
            return enemyData;
        }

      /// <summary>
      /// The function GetEnemyStruct() returns an EnemyDataStruct object with values assigned to its
      /// properties.
      /// </summary>
      /// <returns>
      /// The method is returning an instance of the EnemyDataStruct class.
      /// </returns>
        private EnemyDataStruct GetEnemyStruct() 
        { 
            EnemyDataStruct enemy = new EnemyDataStruct 
            { 
                Name = m_EnemyName,
                Damage = m_EnemyDamageValue,
                Speed = m_EnemySpeedValue,
                _EnemyPrefabName = m_ChooseEnemyData[_EnemyGameObjectIndex].name,

            };
            return enemy;
        }
        #endregion

        public void DeleteButton(int index) 
        { 
            _DeleteEnemy.onClick.RemoveAllListeners();
            _DeleteEnemy.onClick.AddListener(() => { DeleteEnemy(index); });
        
        }

      /// <summary>
      /// The DeleteEnemy function deletes an enemy object at a specified index, removes it from a list,
      /// saves the updated enemy data, and removes all listeners from a button.
      /// </summary>
      /// <param name="index">The index parameter represents the position of the enemy object in the
      /// _SpawnedObjectCreated list that you want to delete.</param>
        private async void DeleteEnemy(int index) 
        {
            Destroy(_SpawnedObjectCreated[index]);
            _SpawnedObjectCreated.RemoveAt(index);
            m_CreateEnemyData.RemoveAt(index);
            await SaveEnemyData.SaveData();
            _DeleteEnemy.onClick.RemoveAllListeners();
        }
      /// <summary>
      /// The function "EnemyCanByCreate" returns true if the enemy's damage value and speed value are
      /// greater than 0 and if the enemy's sprite is not null.
      /// </summary>
      /// <returns>
      /// The method is returning a boolean value.
      /// </returns>
        private bool EnemyCanByCreate() 
        { 
            return m_EnemyDamageValue > 0 && m_EnemySpeedValue > 0 && _EnemyOverview.sprite != null;
            
        }

        private void CheckCreateButton() 
        { 
            if(EnemyCanByCreate()) 
            { 
                m_CreateButtonIni = true;
            }
            else { m_CreateButtonIni = false; }
        
        }


        private void DestroyLoad() 
        {
            _SpawnedObjectCreated.ForEach(obj => { Destroy(obj); });
            _SpawnedObjectCreated.Clear();
            SpawnCreateContainerData();
        }
        private void AddListeners() 
        {
            _EnemyDamage.onValueChanged.AddListener(value => { CheckCreateButton(); });
            _EnemySpeed.onValueChanged.AddListener(value => { CheckCreateButton(); });
            _CreateEnemy.onClick.AddListener(() => { CreateEnemy(); });
        }
        #endregion
    }
}
