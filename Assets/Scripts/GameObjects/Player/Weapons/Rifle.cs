using HCC.GameObjects.GUI;
using HCC.GameObjects.Player;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HCC.GameObjects.Weapons
{
   /* The Rifle class is a subclass of the Weapons class and represents a type of weapon that can
   perform single attacks. */
    public class Rifle : Weapons
    {
        public enum RifleType {SINGLE}
        public Action RifleAttack;

        #region Fields
        [Header("Rifle")]
        [SerializeField] private Transform _BulletPoint;
        public RifleType _RifleType = RifleType.SINGLE;
        #endregion

        #region Properties
        private int m_AvailableAmmo { get => PlayerStats.AvailableAmmo; set { PlayerStats.AvailableAmmo = value; } }
        private GameObject m_Bullet { get => BulletPool.instance?.GetBullet();}
        #endregion

        // Start is called before the first frame update
        void Start()
        {

        }

      /// <summary>
      /// The function "OnShot" is triggered when a shot input is pressed, and it calls the
      /// "SetRifleType" function with a rifle type parameter.
      /// </summary>
      /// <param name="InputValue">The InputValue parameter is an object that represents the input value
      /// of the action being performed. It could be a button press, a joystick movement, or any other
      /// input event. In this case, it is used to check if the input value is pressed (i.e., if the
      /// button is being held</param>
        private void OnShot(InputValue inputValue) 
        { 
            if(inputValue.isPressed) 
            {
                SetRifleType(_RifleType);
                
            }
        }

       /// <summary>
       /// The function SetRifleType sets the attack type of a rifle based on the input RifleType and
       /// performs the corresponding attack.
       /// </summary>
       /// <param name="RifleType">An enum type that represents the type of rifle. It has a value of
       /// either "SINGLE" or another unspecified value.</param>
        public void SetRifleType(RifleType type) 
        { 
            switch (type) 
            {
                case RifleType.SINGLE:
                    RifleAttack = SingleAttackType;
                    RifleAttack();
                    Debug.Log(m_AvailableAmmo);
                    break;
                default:
                    break;
                    
            }
        
        }
      /// <summary>
      /// The function "WeaponAttack" sets the rifle type and performs a rifle attack.
      /// </summary>
      /// <param name="damage">The "damage" parameter is a float value that represents the amount of
      /// damage the weapon attack will deal.</param>
        public override void WeaponAttack(float damage)
        {
            SetRifleType(_RifleType);
            RifleAttack();
            
        }

        private bool CheckAmmo() 
        {
            return m_AvailableAmmo > 0 && m_Bullet != null;
        }
        /// <summary>
        /// The SingleAttackType function checks if there is enough ammo, gets a bullet from the bullet
        /// pool, sets its position and rotation, activates it, decreases the available ammo count, and
        /// destroys the bullet icon in the player GUI.
        /// </summary>
        private void SingleAttackType() 
        {
            
            if (CheckAmmo()) 
            {
                GameObject bullet = BulletPool.instance.GetBullet();
                bullet.transform.position = _BulletPoint.position;
                bullet.transform.rotation = _BulletPoint.rotation;
                bullet.SetActive(true);
                m_AvailableAmmo--;
                PlayerGUIManager.instance.DestroyBulletIcon();
            }
        
        }
    }
}
