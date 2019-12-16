using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TH
{
    public class UIStorySetter : MonoBehaviour
    {
        //public Text m_mainTitle;
        //public Text m_subTitle;
        //public Text m_storyName;
        //public Text m_stroyExplanation;
        public Image m_backgroundImage;
        public SubStoryInfo m_storyInfo;
        public Image m_lockImage;

        public void Awake()
        {
        }

        public void SetData(SubStoryInfo info)
        {
            m_storyInfo = info;

            //m_mainTitle.text = m_storyInfo.m_mainTitle + " 장";
            //m_subTitle.text = "제 " + m_storyInfo.m_subTitle + " 화";
            //m_storyName.text = m_storyInfo.m_storyName;
           // m_stroyExplanation.text = m_storyInfo.m_storyExplanation;
            //m_backgroundImage.sprite = m_storyInfo.m_backGroundSprite;

            m_lockImage.gameObject.SetActive(!m_storyInfo.m_isOpen);
        }

        public void SetBackGroundImage(Sprite image)
        {
            m_backgroundImage.sprite = image;
        }

        public SubStoryInfo GetStoryData()
        {
            return m_storyInfo;
        }

        
    }

}