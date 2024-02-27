

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace HCC.GameObjects.Enemy
{
    [Serializable]
    public struct EnemyDataStruct
    {
        public string Name;
        [TextArea] public string Description;
        [Range(0f,100f)] public float Damage;
        [Range(0f, 100f)] public float Speed;
        public string _EnemyPrefabName;
        [SerializeReference] public IEnemyAttack _EnemyAttackType;
        
        public GameObject m_EnemyPrefab { get => Resources.Load<GameObject>("EnemyEditor_Assets/EnemyEditor_EnemyPrefabs/" + _EnemyPrefabName); }
        public Sprite m_EnemySprite { get => m_EnemyPrefab.GetComponent<SpriteRenderer>().sprite;}
    }
}
