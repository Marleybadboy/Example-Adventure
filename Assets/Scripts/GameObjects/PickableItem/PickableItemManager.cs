
using HCC.GameObjects.Enemy;
using HCC.GameObjects.Player;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace HCC.GameObjects.PickableItem
{
   /* The PickableItemManager class manages the spawning of pickable items in a game, with the ability
   to spawn random items or specific chosen items based on certain conditions. */
    public class PickableItemManager : MonoBehaviour
    {
        public static PickableItemManager Instance;

        #region Fields
        public int _NextSpawnTime;
        public float _CurrentTime;
        public PickableItemData[] _ItemData;
        public UnityEvent _SpawnTimeEvent;
        #endregion

        #region Properties
        PickableItemData m_RandomItemData {  get => _ItemData[UnityEngine.Random.Range(0, _ItemData.Length)]; }
        private float m_SpawnTimeInvoke { get => _NextSpawnTime + (float)PlayerStats.CurrentLevel;}
        #endregion


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _SpawnTimeEvent.AddListener(SpawnRandomItem);
            StaticEvents.onTakeDamage += StaticEvents_onTakeDamage;
            Debug.Log(m_SpawnTimeInvoke);

        }

        private void StaticEvents_onTakeDamage(object sender, int e)
        {
            if(PlayerStats.PlayerHealth == GameManager.Instance._PlayerHealth / 2) 
            {
                SpawnChosenItem(typeof(HealthPotion));
                StaticEvents.onTakeDamage -= StaticEvents_onTakeDamage;
            }
        }

     

        // Update is called once per frame
        void Update()
        {
            _CurrentTime += Time.unscaledDeltaTime;
            SpawnTimeEvent(_CurrentTime);
        }

        private void OnDestroy()
        {
            _SpawnTimeEvent.RemoveAllListeners();
            StaticEvents.onTakeDamage -= StaticEvents_onTakeDamage;
            
        }

        /// <summary>
        /// The function "SpawnTimeEvent" checks if the given time is equal to a specific value and if
        /// so, invokes a spawn time event.
        /// </summary>
        /// <param name="time">The time parameter is a float value representing the current
        /// time.</param>
        private void SpawnTimeEvent(float time) 
        {
            
            if (Mathf.Floor(time).Equals(m_SpawnTimeInvoke)) 
            {
                _CurrentTime = 0;
                _SpawnTimeEvent.Invoke();
                

            }
            
        }
      /// <summary>
      /// The SpawnRandomItem function spawns a random item at a pickable spawn point.
      /// </summary>
        private void SpawnRandomItem() 
        {
            
            Transform trans = GetPickableSpawnPoint();
            Instantiate(m_RandomItemData._PickablePrefab, trans.position, Quaternion.identity, trans);

        }

       /// <summary>
       /// The function "SpawnChosenItem" spawns a pickable item of a specific type based on the
       /// provided item parameter.
       /// </summary>
       /// <param name="T">T is a generic type parameter that represents the type of the item being
       /// spawned. It is constrained to be a Type, meaning it can only be a class that represents a
       /// type.</param>
        private void SpawnChosenItem<T>(T item) where T : Type 
        { 
            foreach(PickableItemData data in _ItemData) 
            { 
                if(data._ItemBehviour.GetType() == item) 
                {
                    Transform trans = GetPickableSpawnPoint();
                    Instantiate(m_RandomItemData._PickablePrefab, trans.position, Quaternion.identity, trans);
                    break;
                }
            
            }
            
        }
       /// <summary>
       /// The function "GetPickableSpawnPoint" returns the first child transform of the current
       /// transform that does not have any child objects.
       /// </summary>
       /// <returns>
       /// The method `GetPickableSpawnPoint()` returns a `Transform` object, which represents the
       /// position and rotation of a game object in Unity.
       /// </returns>
        private Transform GetPickableSpawnPoint() 
        { 
            for (int i = 0; i < transform.childCount; i++) 
            { 
                Transform obj = transform.GetChild(i);
                if(obj.childCount == 0) 
                {
                    return obj;
                }
            
            }
            return null;
        
        }
    }
}
