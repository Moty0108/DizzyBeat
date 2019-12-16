using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TH
{
    public class UIGetMode : MonoBehaviour
    {
        public Image m_target;

        public Sprite m_guitar;
        public Sprite m_base;
        public Sprite m_piano;
        public Sprite m_drum;

        private void OnEnable()
        {
            switch(GameInfoManager.Instance.m_gameInfo.m_mode)
            {
                case MODE.VOCAL:
                    m_target.sprite = m_base;
                    break;

                case MODE.GUITAR:
                    m_target.sprite = m_guitar;
                    break;

                case MODE.KEYBOARD:
                    m_target.sprite = m_piano;
                    break;

                case MODE.DRUM:
                    m_target.sprite = m_drum;
                    break;
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}