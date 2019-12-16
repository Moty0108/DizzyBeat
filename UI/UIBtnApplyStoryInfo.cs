using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH
{

    public class UIBtnApplyStoryInfo : UIButton
    {
        UIStorySetter m_storySetter;
        public UIStoryExplanation m_storyExplanation;
        public string m_sceneName;
        public UIBtnSceneLoad m_sceneLoad;
        public Sprite m_name;
        public Sprite m_level;
        public Sprite m_mode;
     
      
        public override void Click()
        {
            m_storySetter = GetComponentInParent<UIStorySetter>();
            m_name = Resources.Load<Sprite>("Story/" +m_storySetter.m_storyInfo.m_musicName);
            m_level = Resources.Load<Sprite>("Story/" + m_storySetter.m_storyInfo.m_difficulty.ToString());
            m_mode= Resources.Load<Sprite>("Story/" + m_storySetter.m_storyInfo.m_mode.ToString());
            m_storyExplanation.SetData(m_name, m_level, m_mode, m_storySetter.m_storyInfo.m_storyExplanation);
        
            GameInfoManager.Instance.SetStoryCSV(m_storySetter.m_storyInfo.m_storyCSV);
            GameInfoManager.Instance.SetQuestCSV(m_storySetter.m_storyInfo.m_questName);
            GameInfoManager.Instance.SetMode(m_storySetter.m_storyInfo.m_mode);
            GameInfoManager.Instance.SetMusic(m_storySetter.m_storyInfo.m_musicName);
            GameInfoManager.Instance.SetSpeed(1);
            GameInfoManager.Instance.SetDifficulty(m_storySetter.m_storyInfo.m_difficulty);
            GameInfoManager.Instance.SetMusicCSV(m_storySetter.m_storyInfo.m_musicCSV);
            GameInfoManager.Instance.m_gameInfo.m_storyName = m_storySetter.m_storyInfo.m_storyName;
            m_sceneLoad.m_sceneName = m_sceneName;
        }
    }

}