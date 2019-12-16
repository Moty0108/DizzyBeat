using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TH
{

    public class UIStoryInfo : MonoBehaviour, DataUpdateObserver
    {
        public Text m_textQuestName;
        public Text m_textQuestExplanation;
        public Text m_textQuestCount;
        public Image m_imgQuestGuage;

        public void UpdateData()
        {
            m_textQuestName.text = StoryManager.Instance.m_currentStory.m_name;
            m_textQuestCount.text = StoryManager.Instance.m_currentStory.GetClearCount().ToString() + " / " + StoryManager.Instance.m_currentStory.GetSubStoryCount();
            m_imgQuestGuage.fillAmount = (float)StoryManager.Instance.m_currentStory.GetClearCount() / (float)StoryManager.Instance.m_currentStory.GetSubStoryCount();
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