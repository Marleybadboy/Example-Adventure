using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HCC.GameObjects.Enemy
{
    [CreateAssetMenu(menuName = "Enemy Data/Enemy", fileName = "EnemyData")]
    public class EnemyData : ScriptableObject
    {
       public EnemyDataStruct _EnemyData;
       [SerializeReference, SubclassSelector] public IEnemyAttack _EnemyAttack;
    }
}
