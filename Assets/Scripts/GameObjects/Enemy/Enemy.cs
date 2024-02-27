
using UnityEngine;
using UnityEngine.Events;

namespace HCC.GameObjects.Enemy
{
   /* The Enemy class is a MonoBehaviour that represents an enemy in a game, with fields for attack,
   speed, damage, and events for when the enemy dies or is killed. */
    public class Enemy : MonoBehaviour
    {
        #region Fields
        [SerializeReference, SubclassSelector] public IEnemyAttack _Attack;
        public float _Speed;
        public int _Damage;
        public UnityEvent _OnEnemyDead;
        #endregion
        #region Properties
        private Animator m_Animator { get => GetComponent<Animator>(); }
        #endregion
        // Start is called before the first frame update
        void Start()
        {
            _Attack.AssignAnimAttack(AttackAnim);
        }

        // Update is called once per frame
        /// <summary>
        /// The FixedUpdate function moves and attacks using the specified speed and damage values.
        /// </summary>
        void FixedUpdate()
        {
            _Attack.Move(_Speed,transform);
            _Attack.Attack(_Damage,transform);

        }

       /// <summary>
       /// The function sets the "Attack" boolean parameter in the animator to the given value.
       /// </summary>
       /// <param name="value">The value parameter is a boolean value that determines whether the attack
       /// animation should be played or not. If value is true, the attack animation will be played. If
       /// value is false, the attack animation will not be played.</param>
        public void AttackAnim(bool value) 
        {
            m_Animator.SetBool("Attack", value);
        }

        public void Die() 
        { 
            _OnEnemyDead.Invoke();
        }
        public void DestroyEnemyObject() 
        { 
            Destroy(gameObject);
        }

        public void InvokeEnemyKill() 
        {
            StaticEvents.OnEnemyKill(this,0);
        }
    }

}
