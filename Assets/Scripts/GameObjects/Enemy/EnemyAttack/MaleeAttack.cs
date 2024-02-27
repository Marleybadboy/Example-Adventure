
using System;
using Unity.Mathematics;
using UnityEngine;

namespace HCC.GameObjects.Enemy 
{
    [Serializable]
    /* The MaleeAttack class is an implementation of the IEnemyAttack interface that defines methods
    for attacking and moving an enemy character in a game. */
    public class MaleeAttack : IEnemyAttack
    {
        #region Properties
        private Vector2 m_PlayerPostion { get => GameManager.Instance._Player.transform.position; }
        #endregion
        
        public IEnemyAttack.AnimAction animAction;

       /// <summary>
       /// The function "Attack" checks if a given object has a BoxCollider2D component, and if so, it
       /// performs an attack action on any Player objects within the collider's bounds.
       /// </summary>
       /// <param name="damage">The damage parameter is an integer that represents the amount of damage
       /// to be dealt to the target.</param>
       /// <param name="Transform">The Transform parameter represents the transform component of a game
       /// object. It contains information about the position, rotation, and scale of the object in the
       /// scene. In this case, it is used to specify the position of the object that is being
       /// attacked.</param>
        public void Attack(int damage, Transform ob)
        {
            if(ob.TryGetComponent<BoxCollider2D>(out BoxCollider2D collider)) 
            {
                float2 vector = new Vector2(ob.position.x, collider.bounds.center.y);
                float2 bounds = new float2(collider.bounds.size.x, collider.bounds.size.y);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(vector, bounds , 0f);
                foreach(Collider2D col in colliders) 
                {
                    if (col.GetComponent<Player.Player>()) 
                    {
                        StaticEvents.OnTakeDamage(this, damage);
                        animAction(true);
                        break;
                       
                    }
                    animAction(false);
                }
            }
        }

      /// <summary>
      /// The Move function moves an enemy object towards the player at a specified speed.
      /// </summary>
      /// <param name="speed">The speed at which the enemy object should move towards the
      /// player.</param>
      /// <param name="Transform">The Transform parameter represents the transform component of the
      /// enemy object. It is used to access and modify the position and rotation of the enemy
      /// object.</param>
        public void Move(float speed, Transform enemyobj)
        {
            if (GameManager.Instance != null)
            {
                Vector3 playerdir = new Vector3(m_PlayerPostion.x - enemyobj.position.x, m_PlayerPostion.y - enemyobj.position.y);
                Vector3 movment = playerdir.normalized * Time.deltaTime * speed;
                
                enemyobj.localRotation = m_PlayerPostion.x > enemyobj.position.x ? quaternion.RotateY(0f) : quaternion.RotateY(-3.14f);
                enemyobj.position += movment;
               
            }
        }

      /// <summary>
      /// The function AssignAnimAttack assigns an animation action to the animAction delegate.
      /// </summary>
      /// <param name="action">The parameter "action" is of type IEnemyAttack.AnimAction.</param>
        public void AssignAnimAttack(IEnemyAttack.AnimAction action)
        {
            animAction += action;
        }
    }
}
