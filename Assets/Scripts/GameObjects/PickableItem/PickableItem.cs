using DG.Tweening;
using System;
using Unity.Mathematics;
using UnityEngine;

namespace HCC.GameObjects.PickableItem
{
    [Serializable]
    /* The above class is an abstract class in C# that represents a pickable item in a game, with
    properties and methods for handling animations and triggering actions when the item is picked up
    by the player. */
    public abstract class PickableItem : MonoBehaviour
    {
        #region Fields
        public Action _PickableAction;
        public bool _UsePickableAnimation = false;

        [Header("Standard Animation")]
        [SerializeField] private float2 _ScaleEndMove;
        [SerializeField] private float _Duration;
        #endregion

        #region Properties
        private AudioSource m_Source { get => GetComponent<AudioSource>();}
        #endregion

        public virtual void Start () 
        {
            _PickableAction += AssignAction;
            ObjectAnimation();
        
        }

        public virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Player.Player>()) 
            {
                _PickableAction();
            }
        }

        public virtual void AssignAction() 
        { 
            if(m_Source != null) { m_Source.Play(); }
            Destroy(gameObject);
        }

        public virtual void ObjectAnimation() 
        { 
            if(_UsePickableAnimation) 
            {
                StandardAnimation();
            }
            else {OwnAnimation();}
        }

        public virtual void OwnAnimation() { }

        private void StandardAnimation() 
        { 
            Sequence seq = DOTween.Sequence();
            seq.Prepend(transform.DOLocalMoveY(_ScaleEndMove.x, _Duration))
                .Join(transform.DOScaleY(_ScaleEndMove.y, _Duration))
                .SetLoops(-1, LoopType.Yoyo);
        }


    }
}
