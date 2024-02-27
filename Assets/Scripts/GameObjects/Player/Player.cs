
using UnityEngine;

namespace HCC.GameObjects.Player
{
    
/* The Player class in C# handles the player's health and damage-taking functionality. */
    public class Player : MonoBehaviour
    {
        #region Fields
        #endregion
        #region Properties
        private int m_Health { get => PlayerStats.PlayerHealth; set { PlayerStats.PlayerHealth = value;} }
        #endregion
        #region Functions
        // Start is called before the first frame update
        void Start()
        {
            StaticEvents.onTakeDamage += StaticEvents_onTakeDamage;
        }
        private void OnDestroy()
        {
            StaticEvents.onTakeDamage -= StaticEvents_onTakeDamage;
        }

        private void StaticEvents_onTakeDamage(object sender, int damage)
        {
            TakeDamege(damage);
            
        }
        #endregion
        #region Methods
        public void TakeDamege(int damage) 
        { 
            m_Health -= damage/2;
            if (m_Health <= 0) 
            {
                GameManager.Instance?.SetState(GameManager.GameState.FAILURE);
            }

        }


        #endregion


    }
}
