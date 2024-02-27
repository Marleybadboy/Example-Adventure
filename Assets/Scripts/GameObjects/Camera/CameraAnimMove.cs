using DG.Tweening;
using UnityEngine;

namespace HCC.GameObjects.MainMenu
{
   /* The CameraAnimMove class is responsible for animating the movement of a camera between a set of
   predefined positions. */
    public class CameraAnimMove : MonoBehaviour
    {
        [SerializeField] private Transform _CamTrnasform;
        [SerializeField] private Transform[] _Positions;
        [SerializeField] private float _Duration;

        private int m_RandomIndex { get => Random.Range(0, _Positions.Length); }
        private Transform m_RandomPos { get => _Positions[m_RandomIndex];}
        // Start is called before the first frame update
        void Start()
        {
            CameraMoveAnim();
        }

      /// <summary>
      /// The function CameraMoveAnim() uses DOTween to animate the local movement of a camera to a
      /// random position, and then calls itself recursively to create a continuous animation loop.
      /// </summary>
        private void CameraMoveAnim() 
        { 
            Sequence seq = DOTween.Sequence();
            seq.Prepend(_CamTrnasform.DOLocalMove(m_RandomPos.localPosition,_Duration)).OnComplete(() => {CameraMoveAnim();});
        
        }
    }
}
