using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH
{
    public enum Rank
    {
        NONE, D, C, B, A, S
    }

    public class QuestManager : Singleton<QuestManager>
    {
        List<Dictionary<string, object>> m_list;

        public string name;
        public Difficulty dif;
        public Rank rank;
        public int per;
        public int grea;
        public int bad;
        public int miss;
        public int score;
        public int combo;

        // Start is called before the first frame update
        void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);

            m_list = DBManager.Instance.GetQuestData();
        }

        // Update is called once per frame
        void Update()
        {

        }

        [ContextMenu("aa")]
        public void aa()
        {
            CheckQuest(name, dif, rank, per, grea, bad, miss, score, combo);
        }

        public bool CheckQuest(string questName, Difficulty difficulty, Rank rank, int perfect, int great, int bad, int miss, int score, int combo)
        {
            Debug.Log("Great이상 퍼센트 : " + ((float)(perfect + great) / (float)(perfect + great + bad + miss) * 100f) + "%");

            int minValue, maxValue;
            bool isClear = true;

            for(int i = 0; i<m_list.Count;i++)
            {
                if(m_list[i]["QuestName"].ToString() == questName)
                {
                    if ((Difficulty)System.Enum.Parse(typeof(Difficulty), m_list[i]["Difficulty"].ToString()) > difficulty)
                    {
                        Debug.Log(m_list[i]["QuestName"].ToString() + " : 난이도 미달!");
                        isClear = false;
                    }

                    if ((Rank)System.Enum.Parse(typeof(Rank), m_list[i]["Rank"].ToString()) > rank)
                    {
                        Debug.Log(m_list[i]["QuestName"].ToString() + " : 랭크 미달!");
                        isClear = false;
                    }

                    int.TryParse(m_list[i]["MoreThanPerfect"].ToString(), out minValue);
                    int.TryParse(m_list[i]["LessThanPerfect"].ToString(), out maxValue);
                    if ( !(minValue <= perfect && perfect <= maxValue) )
                    {
                        Debug.Log(m_list[i]["QuestName"].ToString() + " : 퍼펙트 미달!");
                        isClear = false;
                    }

                    int.TryParse(m_list[i]["MoreThanGreat"].ToString(), out minValue);
                    int.TryParse(m_list[i]["LessThanGreat"].ToString(), out maxValue);
                    if (!(minValue <= great && great <= maxValue))
                    {
                        Debug.Log(m_list[i]["QuestName"].ToString() + " : 그레이트 미달!");
                        isClear = false;
                    }

                    int.TryParse(m_list[i]["MoreThanBad"].ToString(), out minValue);
                    int.TryParse(m_list[i]["LessThanBad"].ToString(), out maxValue);
                    if (!(minValue <= bad && bad <= maxValue))
                    {
                        Debug.Log(m_list[i]["QuestName"].ToString() + " : 배드 미달!");
                        isClear = false;
                    }

                    int.TryParse(m_list[i]["MoreThanMiss"].ToString(), out minValue);
                    int.TryParse(m_list[i]["LessThanMiss"].ToString(), out maxValue);
                    if (!(minValue <= miss && miss <= maxValue))
                    {
                        Debug.Log(m_list[i]["QuestName"].ToString() + " : 미스 미달!");
                        isClear = false;
                    }

                    int.TryParse(m_list[i]["Score"].ToString(), out maxValue);
                    if (score < maxValue)
                    {
                        Debug.Log(m_list[i]["QuestName"].ToString() + " : 스코어 미달!");
                        isClear = false;
                    }

                    int.TryParse(m_list[i]["Combo"].ToString(), out maxValue);
                    if (combo < maxValue)
                    {
                        Debug.Log(m_list[i]["QuestName"].ToString() + " : 콤보 미달!");
                        isClear = false;
                    }

                    int.TryParse(m_list[i]["GoodPercent"].ToString(), out maxValue);

                    float percent = (float)(perfect + great) / (float)(perfect + great + bad + miss) * 100f;

                    if (percent < maxValue)
                    {
                        Debug.Log(m_list[i]["QuestName"].ToString() + " : 퍼센트 미달!");
                        isClear = false;
                    }


                    if(isClear)
                    {
                        Debug.Log(questName + " 달성!!!!!!!!!!!!!");
                    }
                    else
                    {
                        Debug.Log(questName + " 달성 실패...");
                    }

                    return isClear;
                    
                    //UserInfoDBManager.Instance.m_user.m_storyInfos[m_list[i]["OpenStory"].ToString()].m_isOpen = true;
                }
            }

            Debug.Log("해당 퀘스트 정보 없음");
            return false;
        }
    }
}
