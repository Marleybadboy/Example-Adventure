

using HCC.GameObjects.Player;
using System.Collections;
using UnityEngine;

namespace HCC.GameObjects.Weapons
{
  /* The Bullet class is responsible for controlling the movement and behavior of a bullet in a game. */
    public class Bullet : MonoBehaviour
    {
        #region Fields
        public float _BulletSpeed, _BulletTime;
        #endregion
        private Rigidbody2D m_RbBullet {get => GetComponent<Rigidbody2D>();}

        // Start is called before the first frame update
        void Start()
        {
            
        }
        private void OnEnable()
        {
            StartCoroutine(nameof(ShotTime));
        }
        // Update is called once per frame
        void Update()
        {
            Shot();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Enemy.Enemy>())
            {
                collision.GetComponent<Enemy.Enemy>().Die();
                PlayerStats.KilledEnemies += 1;
                StaticEvents.OnEnemyKill(this, PlayerStats.KilledEnemies);
                StopAllCoroutines();
                gameObject.SetActive(false);
            }
        }

        public void Shot() 
        {
            m_RbBullet.velocity = transform.right * _BulletSpeed;
        }

        IEnumerator ShotTime() 
        {
            yield return new WaitForSeconds(_BulletTime);
            gameObject.SetActive(false);
        }
    }
}
