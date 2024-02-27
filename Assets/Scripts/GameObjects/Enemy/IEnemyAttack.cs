

using UnityEngine;

namespace HCC.GameObjects.Enemy
{
    
   /* The code is defining an interface called `IEnemyAttack`. */
    public interface IEnemyAttack 
    {
        public void Attack(int damage, Transform obj);
        public void Move(float speed, Transform enemyobj);
        public void AssignAnimAttack(AnimAction action);


       /* The line `public delegate void AnimAction(bool value);` is declaring a delegate type called
       `AnimAction`.  In this case, the `AnimAction` delegate takes a single parameter of type
       `bool` and does not return a value. It can be used to define a method that takes a `bool`
       parameter and performs some action. */
        public delegate void AnimAction(bool value);

    }
}
