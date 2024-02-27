
using DG.Tweening;
using HCC.GameObjects.Player;
using UnityEngine;

namespace HCC.GameObjects.Weapons {
  /* The Saber class is a subclass of the Weapons class and represents a weapon that can be used to
  attack enemies in a game. */
    public class Saber : Weapons
    {
        #region Properties
        private PolygonCollider2D m_SaberCollider { get => _WeaponObject.GetComponent<PolygonCollider2D>(); }
        #endregion
        // Start is called before the first frame update
        void Start()
        {
            m_WeaponVisible = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Enemy.Enemy>()) 
            {
                collision.GetComponent<Enemy.Enemy>().Die();
                PlayerStats.KilledEnemies += 1;
                StaticEvents.OnEnemyKill(this, PlayerStats.KilledEnemies);

            }
        }

        public override void WeaponAttack(float damage)
        {
            
            AttackSeq();
        }

       /// <summary>
       /// The function AttackSeq() creates a DOTween sequence that moves and rotates a weapon object to
       /// a specified end point, with a delay and a callback function to start an animation point.
       /// </summary>
       /// <returns>
       /// The method is returning a Sequence object.
       /// </returns>
        public  Sequence AttackSeq()
        {
            Sequence seq = DOTween.Sequence();
            
            m_WeaponVisible = true;
            seq
                .Prepend(_WeaponObject.transform.DOLocalMove(_EndPoint.localPosition, _Duration))
                .Join(_WeaponObject.transform.DOLocalRotate(_EndPoint.localEulerAngles, _Duration))
                .SetDelay(_Delay)
                .OnComplete(() => { m_WeaponVisible = false; StartAnimaPoint();});
            return seq;

        }
        private void StartAnimaPoint() 
        {
            _WeaponObject.transform.position = _StartPoint.position;
            _WeaponObject.transform.rotation = _StartPoint.rotation;
        }
        
    }
}
