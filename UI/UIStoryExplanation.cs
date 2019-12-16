using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TH
{
    public class UIStoryExplanation : MonoBehaviour
    {
        public Image m_musicName;
        public Image m_musicLevel;
        public Image m_musicMode;
        public Text m_musicExplanation;

        public void SetData(Sprite name, Sprite level, Sprite mode, string explanation)
        {
           
            m_musicName.sprite = name;
            m_musicLevel.sprite = level;
            m_musicMode.sprite = mode;
            m_musicExplanation.text = explanation;
        }
    }

}