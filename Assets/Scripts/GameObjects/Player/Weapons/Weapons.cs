
using DG.Tweening;
using UnityEngine;

namespace HCC.GameObjects.Weapons
{
 /* The "Weapons" class is an abstract class that represents a weapon in a game, with properties and
 methods for weapon stats, animation, and attacking. */
    public abstract class Weapons : MonoBehaviour
    {
        #region Fields
        [Header("Weapon Stats")]
        public GameObject _WeaponObject;
        public float _Damage;
        [Header("Animation")]
        public Transform _StartPoint;
        public Transform _EndPoint;
        public float _Duration;
        public float _Delay;
        #endregion
        #region Properties
        public bool m_WeaponVisible { set { _WeaponObject.SetActive(value); } }
        #endregion
        #region Methods
        public virtual void WeaponAttack(float damage) 
        { 
        
        }
        #endregion
    }
}
