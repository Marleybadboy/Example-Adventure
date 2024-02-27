using DG.Tweening;
using HCC.GameObjects.Player;
using TMPro;
using UnityEngine;

namespace HCC.GameObjects.GUI
{
    /* The InfoPanelManager class manages the display of level information in a UI panel, including
    animation effects. */
    public class InfoPanelManager : MonoBehaviour
    {
        public static InfoPanelManager instance;
        #region Fields
        [Header("Level")]
        [SerializeField] private TextMeshProUGUI _LevelInfo;
        [SerializeField] private string _Message;

        [Header("Animation values")]
        [SerializeField] private float _Duration;
        [SerializeField] private float _Delay;
        #endregion

        #region Properties
        private string m_InfoMassage { get => $"{_Message} {PlayerStats.CurrentLevel}"; }
        private bool m_IsActive {set => _LevelInfo.gameObject.SetActive(value); }
        #endregion

        private void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            m_IsActive = false;
        }

        public void ShowLevel() 
        {
            LevelText();
        }
        #region Animation
       /// <summary>
       /// The LevelText function updates a text field with a given message using a tween animation.
       /// </summary>
        private void LevelText()
        {
            m_IsActive = true;
            string text = "";

            Tween tween = DOTween.To(() => text, x => text = x, m_InfoMassage, m_InfoMassage.Length / _Duration)
                .OnUpdate(() => { _LevelInfo.SetText(text); }).OnComplete(() => m_IsActive = false);
        }
        #endregion
    }   
}
