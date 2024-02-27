using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCC.GameObjects.Weapons
{

  /* The BulletPool class is responsible for managing a pool of bullets that can be retrieved and used
  in a game. */
    public class BulletPool : MonoBehaviour
    {
        #region Fields
        public static BulletPool instance;
        [Header("Bullet Asset")]
        [SerializeField] private GameObject _BulletPrefab;

        public Bullet[] _Bullets;
        #endregion

        #region Properties
        private int _PlayerAmmo { get => GameManager.Instance._PlayerAmmo; }
        #endregion
        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
             _Bullets = CreateBullets().ToArray();
        }


     /// <summary>
     /// The function creates a list of Bullet objects by instantiating a bullet prefab and adding it to
     /// the list.
     /// </summary>
     /// <returns>
     /// The method is returning a List of Bullet objects.
     /// </returns>
        private List<Bullet> CreateBullets() 
        { 
            List<Bullet> result = new List<Bullet>();
            for(int i = 0; i < _PlayerAmmo; i++) 
            {
                GameObject obj = Instantiate(_BulletPrefab, transform);
                result.Add(obj.GetComponent<Bullet>());
                obj.SetActive(false);
                
            }
            return result;
        
        }

     /// <summary>
     /// The function "GetBullet" returns a GameObject from a list of bullets that is not currently
     /// active in the hierarchy.
     /// </summary>
     /// <returns>
     /// The method is returning a GameObject.
     /// </returns>
        public GameObject GetBullet() 
        { 
            foreach(Bullet bullet in _Bullets) 
            { 
                if(!bullet.gameObject.activeInHierarchy) 
                { 
                    return bullet.gameObject;
                    
                }
            
            }
            return null;
        
        }
    }
}
