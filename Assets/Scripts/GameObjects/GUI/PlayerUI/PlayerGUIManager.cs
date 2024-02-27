using HCC.GameObjects.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HCC.GameObjects.GUI
{
    /* The PlayerGUIManager class manages the player's health, enemy kill count, and ammo display in
    the game's GUI. */
    public class PlayerGUIManager : MonoBehaviour
    {
        #region Fields
        public static PlayerGUIManager instance;
        
        [Header("Player Health Objects")]
        [SerializeField] private Image _FillImage;
        [SerializeField] private TextMeshProUGUI _Counter;
        
        [Header("Player Enemy Counter")]
        [SerializeField] private TextMeshProUGUI _KillCounter;

        [Header("Player Ammo")]
        [SerializeField] private RectTransform _Parent;
        [SerializeField] private GameObject _AmmoIconPrefab;
        #endregion
        #region Properties
        private int m_PlayerActualHealth { get => PlayerStats.PlayerHealth; }
        private int m_StartHealth { get => GameManager.Instance._PlayerHealth; }
        private float m_HealthFillAmount { get => _FillImage.fillAmount; set { _FillImage.fillAmount = value; } }
        private int m_HealthPercentage {set { _Counter.text = $"{value}%"; } }
        private int m_AmmoAvailable { get => PlayerStats.AvailableAmmo; }
        public int m_KillCounter {set { _KillCounter.text = value.ToString(); } }
        #endregion

        private void Awake()
        {
            instance = this;
        }
        // Start is called before the first frame update
      /// <summary>
      /// The Start function subscribes to various events and initializes the starting health.
      /// </summary>
        void Start()
        {
           StaticEvents.onTakeDamage += StaticEvents_onTakeDamage;
           StaticEvents.onEnemyKill += StaticEvents_onEnemyKill;
           StaticEvents.onLoadGame += StaticEvents_onLoadGame;
           InitializeStartHealth();
        }

        private void StaticEvents_onLoadGame(object sender, System.EventArgs e)
        {
            m_KillCounter = PlayerStats.KilledEnemies;
            CalulateHealth();
            Debug.Log(m_PlayerActualHealth + " " + m_StartHealth);
        }

        private void StaticEvents_onEnemyKill(object sender, int counter)
        {
            m_KillCounter = PlayerStats.KilledEnemies;
        }

        private void OnDestroy()
        {
            StaticEvents.onTakeDamage -= StaticEvents_onTakeDamage;
            StaticEvents.onEnemyKill -= StaticEvents_onEnemyKill;
            StaticEvents.onLoadGame -= StaticEvents_onLoadGame;
        }

        private void StaticEvents_onTakeDamage(object sender, int e)
        {
            CalulateHealth();
        }

        #region Methods
        public void InitializeStartHealth() 
        { 
            m_HealthFillAmount = 1f;
            m_HealthPercentage = 100;
        }

       /// <summary>
       /// The InitializeAmmo function initializes the ammo by clearing any existing ammo icons and
       /// instantiating new ones based on the given ammo value.
       /// </summary>
       /// <param name="ammovalue">The parameter "ammovalue" is an integer that represents the number of
       /// ammo icons to be initialized.</param>
        public void InitializeAmmo(int ammovalue) 
        { 
            if(_Parent.childCount > 0) 
            { 
                ClearAmmo();
            }
            for(int i =0;  i < ammovalue; i++) 
            {
                Instantiate(_AmmoIconPrefab, _Parent);
            }
        
        }

        private void ClearAmmo() 
        { 
            for(int i = 0;  i < _Parent.childCount; i++) 
            { 
                Destroy(_Parent.GetChild(i).gameObject);
            
            }
        
        }
        private bool CheckAmmo() 
        {
            return _Parent.childCount.Equals(m_AmmoAvailable);
        }
        public void DestroyBulletIcon() 
        {
           
            if( _Parent.childCount > 0) 
            {
                
                Destroy(_Parent.GetChild(_Parent.childCount -1).gameObject);
            }
    
        }

       /// <summary>
       /// The function calculates the health fill amount and percentage based on the player's actual
       /// health and starting health.
       /// </summary>
        public void CalulateHealth() 
        {
            float updateHealth =  (float)m_PlayerActualHealth/(float)m_StartHealth;
            int updatePercentage = Mathf.FloorToInt(updateHealth * 100);
            m_HealthFillAmount = updateHealth;
            m_HealthPercentage = updatePercentage;
        }
        #endregion
    }
}
