using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TH
{

    [RequireComponent(typeof(Button))]
    public abstract class UIButton : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
    {
        protected Button m_button;

        [SerializeField]
        protected AudioClip m_audioClip;

        [SerializeField]
        protected UIAnimation m_pressAnimation;

        bool m_isPressed = false;

        private void Awake()
        {
            m_button = GetComponent<Button>();
            m_button.onClick.AddListener(Click);
            m_button.onClick.AddListener(ClickSoundPlay);
        }

        private void ClickSoundPlay()
        {
            if(m_audioClip && SoundManager.Instance)
                SoundManager.Instance.PlayAudio(AudioType.EFFECT, m_audioClip);
        }

        

        public abstract void Click();

        public void OnPointerDown(PointerEventData eventData)
        {
            m_isPressed = true;

            if(m_pressAnimation)
                m_pressAnimation.StartPressAnimation(this, transform);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(m_isPressed)
            {
                if(m_pressAnimation)
                    m_pressAnimation.StartExitAnimation(this, transform);
                m_isPressed = false;
            }

        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (m_isPressed)
            {
                if (m_pressAnimation)
                    m_pressAnimation.StartExitAnimation(this, transform);
                m_isPressed = false;
            }
        }
    }

}