using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TH
{

    public class StoryLoaderManager : MonoBehaviour
    {

        public bool m_debugMode = false;

        List<Dictionary<string, object>> m_list;
        public string m_storyData;

        public StoryObject m_storyObject;

        public Sprite[] m_backGrounds;
        public Sprite[] m_leftSprites;
        public Sprite[] m_middleSprites;
        public Sprite[] m_rightSprites;


        public int m_currentIndex = 0;

        bool m_isAnimation = false;
        private void Awake()
        {
            if(!m_debugMode)
            {
                m_storyData = GameInfoManager.Instance.GetStoryCSV();
            }

            m_list = CSVReader.Read("StoryTable/" + m_storyData);

            m_backGrounds = new Sprite[m_list.Count];
            m_leftSprites = new Sprite[m_list.Count];
            m_middleSprites = new Sprite[m_list.Count];
            m_rightSprites = new Sprite[m_list.Count];

            for(int i = 0; i<m_list.Count;i++)
            {
                if (m_list[i]["BackGround"].ToString() != "null" || m_list[i]["BackGround"].ToString() != "NULL")
                    m_backGrounds[i] = (Sprite)Resources.Load<Sprite>("BackGroundImage/" + m_list[i]["BackGround"].ToString());
                else
                    m_backGrounds[i] = null;
            }

            for (int i = 0; i < m_list.Count; i++)
            {
                if (m_list[i]["LeftSprite"].ToString() != "null" || m_list[i]["BackGround"].ToString() != "NULL")
                    m_leftSprites[i] = (Sprite)Resources.Load<Sprite>("StoryImage/" + m_list[i]["LeftSprite"].ToString());
                else
                    m_leftSprites[i] = null;
            }

            for (int i = 0; i < m_list.Count; i++)
            {
                if (m_list[i]["MiddleSprite"].ToString() != "null" || m_list[i]["BackGround"].ToString() != "NULL")
                    m_middleSprites[i] = (Sprite)Resources.Load<Sprite>("StoryImage/" + m_list[i]["MiddleSprite"].ToString());
                else
                    m_middleSprites[i] = null;
            }

            for (int i = 0; i < m_list.Count; i++)
            {
                if (m_list[i]["RightSprite"].ToString() != "null" || m_list[i]["BackGround"].ToString() != "NULL")
                    m_rightSprites[i] = (Sprite)Resources.Load<Sprite>("StoryImage/" + m_list[i]["RightSprite"].ToString());
                else
                    m_rightSprites[i] = null;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            StartStory();
        }


        public void StartStory()
        {
            Debug.Log("스토리 시작!");

            m_currentIndex = 0;
            SetStoryNext();
        }

        [ContextMenu("Next")]
        public void SetStoryNext()
        {
            if(m_currentIndex >= m_list.Count)
            {
                EndStory();
                return;
            }

            if (!m_isAnimation)
            {
                string name = "";
                if (m_list[m_currentIndex].ContainsKey("Talker"))
                    name = m_list[m_currentIndex]["Talker"].ToString();


                m_storyObject.SetData(m_backGrounds[m_currentIndex], m_leftSprites[m_currentIndex], m_middleSprites[m_currentIndex], m_rightSprites[m_currentIndex], m_list[m_currentIndex]["Script"].ToString(), name, ()=> { m_currentIndex++; m_isAnimation = false; });
                m_isAnimation = true;
            }
            else
            {
                //m_storyObject.SetScript(m_list[m_currentIndex]["Script"].ToString());
                //
                var taehyun = m_list[m_currentIndex]["Script"].ToString();
                taehyun.Replace("^", "\n");
                //Debug.Log(taehyun);
                m_storyObject.SetScript(taehyun);
               //팀장이 만든 코드
               m_currentIndex++;
                m_isAnimation = false;
            }
        }

        public void EndStory()
        {
            Debug.Log("스토리 끝!");

            switch (GameInfoManager.Instance.m_gameInfo.m_mode)
            {
                case MODE.DRUM:
                    SceneManager.LoadScene("Type3");
                    break;

                case MODE.GUITAR:
                    SceneManager.LoadScene("Type2");
                    break;

                case MODE.KEYBOARD:
                    SceneManager.LoadScene("Type4");
                    break;

                case MODE.VOCAL:
                    SceneManager.LoadScene("Type1");
                    break;
            }

            Skip();
        }

        public void Skip()
        {
            Debug.Log("스킵!!");
            switch (GameInfoManager.Instance.m_gameInfo.m_mode)
            {
                case MODE.DRUM:
                    SceneManager.LoadScene("Type3");
                    break;

                case MODE.GUITAR:
                    SceneManager.LoadScene("Type2");
                    break;

                case MODE.KEYBOARD:
                    SceneManager.LoadScene("Type4");
                    break;

                case MODE.VOCAL:
                    SceneManager.LoadScene("Type1");
                    break;
            }
        }
    }
}