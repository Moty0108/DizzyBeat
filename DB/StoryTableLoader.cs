using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH
{
    class UISetterList
    {
        public UIMainStorySetter m_mainSetter;
        public List<UIStorySetter> m_subSetter = new List<UIStorySetter>();
    }

    public class StoryTableLoader : MonoBehaviour, DataUpdateObserver
    {
        public GameObject m_contentMainPrefab;
        public GameObject m_contentSubPrefab;
        public GameObject m_explanationWindow;
        public GameObject m_helper;
        List<Dictionary<string, object>> m_list;

        GameObject curMain;

        List<MainStoryInfo> m_storyInfos;
        List<UISetterList> m_storyList;
      
        private void OnEnable()
        {
            UpdateData();
            GameInfoManager.Instance.m_gameInfo.m_helpnum = -1;
            m_helper.SetActive(false);
        }
        private void OnDisable()
        {
            m_helper.SetActive(true);
        }

        public void UpdateData()
        {
            if (m_storyList != null)
            {
                for (int i = 0; i < m_storyList.Count; i++)
                {
                    m_storyList[i].m_mainSetter.SetData(m_storyInfos[i]);


                    for (int j = 0; j < m_storyList[i].m_subSetter.Count; j++)
                    {
                        m_storyList[i].m_subSetter[j].SetData(m_storyInfos[i].m_subStorys[j]);
                    }
                }
            }
        }

        // Start is called before the first frame update
        void Awake()
        {
            
            
            m_storyInfos = StoryManager.Instance.m_mainStorys;
            m_storyList = new List<UISetterList>();

            for(int i = 0; i < m_storyInfos.Count; i++)
            {
                GameObject mainStory = Instantiate(m_contentMainPrefab);
                mainStory.transform.SetParent(transform);
                mainStory.name = m_storyInfos[i].m_name;
                //temp.GetComponentInChildren<UIStorySetter>().SetData();
                mainStory.GetComponentInChildren<UIMainStorySetter>().SetData(m_storyInfos[i]);
                mainStory.GetComponentInChildren<UIMainStorySetter>().SetBackGroundImage(m_storyInfos[i].m_backGroundSprite);
                mainStory.transform.localScale = Vector3.one;
                UISetterList temp = new UISetterList();
                temp.m_mainSetter = mainStory.GetComponentInChildren<UIMainStorySetter>();

                m_storyList.Add(temp);
                for(int j = 0; j < m_storyInfos[i].m_subStorys.Count;j++)
                {
                    GameObject subStory = Instantiate(m_contentSubPrefab);
                    subStory.transform.SetParent(transform);
                    subStory.name = m_storyInfos[i].m_subStorys[j].m_name;
                    subStory.GetComponentInChildren<UIStorySetter>().SetData(m_storyInfos[i].m_subStorys[j]);
                    subStory.GetComponentInChildren<UIStorySetter>().SetBackGroundImage(m_storyInfos[i].m_subStorys[j].m_backGroundSprite);
                    subStory.GetComponentInChildren<UIBtnApplyStoryInfo>().m_storyExplanation = m_explanationWindow.GetComponent<UIStoryExplanation>();
                    subStory.GetComponentInChildren<UIBtnApplyStoryInfo>().m_sceneName = m_storyInfos[i].m_subStorys[j].m_sceneName;
                    subStory.GetComponentInChildren<UIBtnApplyStoryInfo>().m_sceneLoad = m_explanationWindow.GetComponentInChildren<UIBtnSceneLoad>();
                    subStory.GetComponentInChildren<UIOpenPanel>().m_onTargets = new GameObject[] { m_explanationWindow };
                    mainStory.GetComponentInChildren<UIShowToggle>().AddOffArray(subStory);
                    subStory.transform.localScale = Vector3.one;
                    subStory.SetActive(false);
                    //m_storyInfos[i].m_subStorys[j]

                    m_storyList[i].m_subSetter.Add(subStory.GetComponentInChildren<UIStorySetter>());
                }
            }

            int size = 0;

            //for(int i = 0; i<m_list.Count;i++)
            //{
            //    if(m_list[i]["StoryType"].ToString() == "MAIN")
            //    {
            //        GameObject temp = Instantiate(m_contentMainPrefab);
            //        temp.transform.SetParent(transform);
            //        temp.transform.GetComponent<RectTransform>().sizeDelta = new Vector3(512, 288);
            //        temp.name = m_list[i]["StoryName"].ToString();
            //        curMain = temp;
                    
            //        //temp.GetComponentInChildren<UIStorySetter>().SetData(m_list[i]["StoryName"].ToString(), m_list[i]["StoryCSV"].ToString(), m_list[i]["Reward"].ToString());
            //        foreach(UnityEngine.UI.Image image in temp.GetComponentsInChildren<UnityEngine.UI.Image>())
            //        {
            //            if(image.name == "Lock")
            //            {
            //                image.gameObject.SetActive(!UserInfoDBManager.Instance.m_user.m_storyInfos[m_list[i]["StoryName"].ToString()].m_isOpen);
            //            }
            //        }
            //        size++;
            //    }
            //    else if (m_list[i]["StoryType"].ToString() == "SUB")
            //    {
            //        GameObject temp = Instantiate(m_contentSubPrefab);
            //        temp.transform.SetParent(transform);
            //        temp.transform.GetComponent<RectTransform>().sizeDelta = new Vector3(512, 288);
            //        temp.name = m_list[i]["StoryName"].ToString();
            //        curMain.GetComponentInChildren<UIShowToggle>().AddOffArray(temp);
            //        //temp.GetComponent<UIStorySetter>().SetData(m_list[i]["StoryName"].ToString(), m_list[i]["StoryCSV"].ToString(), m_list[i]["Reward"].ToString());
            //        size++;
            //    }


            //}

            //GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, size * 288);

        }
    }

}