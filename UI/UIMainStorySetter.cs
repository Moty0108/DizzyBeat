using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TH
{
    public class UIMainStorySetter : MonoBehaviour
    {
  
        public Image m_backgroundImage;
        public MainStoryInfo m_storyInfo;
        public Image m_lockImage;

        public void Awake()
        {
        }

        public void SetData(MainStoryInfo info)
        {
            m_storyInfo = info;

            //m_backgroundImage.sprite = m_storyInfo.m_backGroundSprite;

            m_lockImage.gameObject.SetActive(!m_storyInfo.m_isOpen);
        }

        public void SetBackGroundImage(Sprite image)
        {
            m_backgroundImage.sprite = image;
        }

        public MainStoryInfo GetStoryData()
        {
            return m_storyInfo;
        }

        
    }

}