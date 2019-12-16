using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TH
{

    public class UIRadioButton : MonoBehaviour
    {
        public int m_startIndex;

        public Button[] m_buttons;

        // Start is called before the first frame update
        void Start()
        {
        }
        private void OnEnable()
        {
            //m_buttons[m_startIndex].onClick.Invoke();
            m_buttons[m_startIndex].GetComponent<UIButton>().Click();
            SelectBtn(m_startIndex);
        }

        public void SelectBtn(int index)
        {
            for (int i = 0; i < m_buttons.Length; i++)
            {
                if (i == index)
                {
                    m_buttons[i].GetComponent<UIRadioButtonObject>().Select();
                    continue;
                }

                m_buttons[i].GetComponent<UIRadioButtonObject>().NonSelect();
            }
        }

        public void TouchSpeedButton(int index)
        {
            GameInfoManager.Instance.m_gameInfo.m_gamespeed = (TH.GameSpeed)index;
        }

    }
}