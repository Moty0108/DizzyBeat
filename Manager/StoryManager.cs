using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH
{
    [System.Serializable]
    public class MainStoryInfo
    {
        public string m_name;
        public List<SubStoryInfo> m_subStorys;
        public bool m_isClear;
        public bool m_isOpen;
        public string m_reward;
        public Sprite m_backGroundSprite;

        public MainStoryInfo(string name)
        {
            m_name = name;
            m_subStorys = new List<SubStoryInfo>();
            m_isClear = false;
            m_isOpen = false;
        }

        public void AddSubStory(SubStoryInfo subStory)
        {
            m_subStorys.Add(subStory);
        }

        public int GetSubStoryCount()
        {
            return m_subStorys.Count;
        }

        public int GetClearCount()
        {
            int count = 0;

            for(int i= 0; i<m_subStorys.Count;i++)
            {
                if(m_subStorys[i].m_isClear)
                {
                    count++;
                }
            }

            return count;
        }
    }

    [System.Serializable]
    public class SubStoryInfo
    {
        public string m_name;
        public string m_storyCSV;
        public string m_questName;
        public bool m_isClear=false;
        public bool m_isOpen;
        public string m_musicName;
        public MODE m_mode;
        public Difficulty m_difficulty;
        public string m_musicCSV;

        public string m_mainTitle;
        public string m_subTitle;
        public string m_storyName;
        public string m_storyExplanation;
        public string m_sceneName;
        public Sprite m_backGroundSprite;

        public SubStoryInfo(string name)
        {
            m_name = name;
        }
    }

    public class StoryManager : Singleton<StoryManager>, DataUpdateObserver, DataInitObserver
    {
        List<Dictionary<string, object>> m_list;
        public List<MainStoryInfo> m_mainStorys;

        public MainStoryInfo m_currentStory;

        public StoryTableLoader m_storyTable;
        public string test;
        public bool a;

        [ContextMenu("asdsad")]
        public void aa()
        {
            SetStoryIsClear(test, a);
        }

        [ContextMenu("zxczxc")]
        public void OpenFirstStory()
        {
            UserInfoDBManager.Instance.SetStoryIsOpen(m_mainStorys[0].m_name, true);
            UserInfoDBManager.Instance.SetStoryIsOpen(m_mainStorys[0].m_subStorys[0].m_storyName, true);
            UpdateData();
        }

        public void SetStoryIsClear(string storyName, bool isClear)
        {            
            for(int i = 0; i< m_mainStorys.Count;i++)
            {
                for(int j = 0; j < m_mainStorys[i].m_subStorys.Count;j++)
                {
                    if (m_mainStorys[i].m_subStorys[j].m_name == storyName)
                    {
                        Debug.Log("aaa");
                        Debug.Log(m_mainStorys[i].m_subStorys[j].m_storyName);
                        UserInfoDBManager.Instance.SetStoryIsClear(m_mainStorys[i].m_subStorys[j].m_storyName, isClear);
                        UserInfoDBManager.Instance.SetStoryIsOpen(m_mainStorys[i].m_subStorys[j].m_storyName, true);
                        if (j + 1 < m_mainStorys[i].m_subStorys.Count)
                        {
                            UserInfoDBManager.Instance.SetStoryIsOpen(m_mainStorys[i].m_subStorys[j + 1].m_storyName, isClear);
                        }
                        else
                        {
                            UserInfoDBManager.Instance.SetStoryIsOpen(m_mainStorys[Mathf.Clamp(i + 1, 0, m_mainStorys.Count - 1)].m_name, isClear);
                        }
                    }
                }
            }

            UpdateData();
        }

        public void UpdateData()
        {
            m_list = DBManager.Instance.GetStoryData();

            for(int i = 0; i< m_mainStorys.Count;i++)
            {
                m_mainStorys[i].m_isClear = UserInfoDBManager.Instance.m_user.m_storyInfos[m_mainStorys[i].m_name].m_isClear;
                m_mainStorys[i].m_isOpen = UserInfoDBManager.Instance.m_user.m_storyInfos[m_mainStorys[i].m_name].m_isOpen;

                for(int j = 0; j < m_mainStorys[i].m_subStorys.Count;j++)
                {
                    m_mainStorys[i].m_subStorys[j].m_isClear = UserInfoDBManager.Instance.m_user.m_storyInfos[m_mainStorys[i].m_subStorys[j].m_name].m_isClear;
                    m_mainStorys[i].m_subStorys[j].m_isOpen = UserInfoDBManager.Instance.m_user.m_storyInfos[m_mainStorys[i].m_subStorys[j].m_name].m_isOpen;
                }
            }

            m_storyTable.UpdateData();
        }

        
        public void InitData()
        {
            m_list = DBManager.Instance.GetStoryData();
            m_mainStorys = new List<MainStoryInfo>();

            for (int i = 0; i < m_list.Count; i++)
            {
                if (m_list[i]["StoryType"].ToString() == "MAIN")
                {
                    MainStoryInfo mainInfo = new MainStoryInfo(m_list[i]["StoryName"].ToString());
                    mainInfo.m_isClear = UserInfoDBManager.Instance.m_user.m_storyInfos[m_list[i]["StoryName"].ToString()].m_isClear;
                    mainInfo.m_isOpen = UserInfoDBManager.Instance.m_user.m_storyInfos[m_list[i]["StoryName"].ToString()].m_isOpen;
                    mainInfo.m_reward = m_list[i]["Reward"].ToString();
                    mainInfo.m_backGroundSprite = (Sprite)Resources.Load("StoryTableBackGroundImage/" + m_list[i]["BackGround"].ToString(), typeof(Sprite));


                    m_mainStorys.Add(mainInfo);
                }

                if (m_list[i]["StoryType"].ToString() == "SUB")
                {
                    for (int j = 0; j < m_mainStorys.Count; j++)
                    {
                        if (m_mainStorys[j].m_name == m_list[i]["MainStoryName"].ToString())
                        {
                            SubStoryInfo subInfo = new SubStoryInfo(m_list[i]["StoryName"].ToString());
                            subInfo.m_storyName = m_list[i]["StoryName"].ToString();
                            subInfo.m_isClear = UserInfoDBManager.Instance.m_user.m_storyInfos[m_list[i]["StoryName"].ToString()].m_isClear;
                            subInfo.m_isOpen = UserInfoDBManager.Instance.m_user.m_storyInfos[m_list[i]["StoryName"].ToString()].m_isOpen;
                            subInfo.m_storyCSV = m_list[i]["StoryCSV"].ToString();
                            subInfo.m_questName = m_list[i]["QuestName"].ToString();
                            subInfo.m_musicName = m_list[i]["MusicName"].ToString();
                            if(m_list[i]["Difficulty"].ToString() != "NULL")
                                subInfo.m_difficulty = (Difficulty)System.Enum.Parse(typeof(Difficulty), m_list[i]["Difficulty"].ToString());
                            if (m_list[i]["Mode"].ToString() != "NULL")
                                subInfo.m_mode = (MODE)System.Enum.Parse(typeof(MODE), m_list[i]["Mode"].ToString());
                            subInfo.m_musicCSV = m_list[i]["MusicCSV"].ToString();
                            subInfo.m_storyExplanation = m_list[i]["Explanation"].ToString();
                            subInfo.m_backGroundSprite = Resources.Load<Sprite>("StoryTableBackGroundImage/" + m_list[i]["BackGround"].ToString());
                            subInfo.m_sceneName = m_list[i]["SceneName"].ToString();
                            m_mainStorys[j].m_subStorys.Add(subInfo);
                        }
                    }

                }
            }

            m_currentStory = m_mainStorys[0];
            
            OpenFirstStory();
        }
    }

}