using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TH
{

    [System.Serializable]
    public class UserInfo
    {
        public string m_userName;

        public Dictionary<string, UserMusicInfo>[] m_musicInfos;

        public Dictionary<string, BackGroundInfo> m_backGroundInfos;

        public Dictionary<string, StoryInfo> m_storyInfos;


        public UserInfo(string name, List<Dictionary<string, object>> musiclist)
        {
            
            m_userName = name;
            m_musicInfos = new Dictionary<string, UserMusicInfo>[4];

            m_backGroundInfos = new Dictionary<string, BackGroundInfo>();
            m_storyInfos = new Dictionary<string, StoryInfo>();

            for (int j = 0; j < m_musicInfos.Length; j++)
            {
                m_musicInfos[j] = new Dictionary<string, UserMusicInfo>();
                for (int i = 0; i < musiclist.Count; i++)
                {
                    m_musicInfos[j].Add(musiclist[i]["MusicName"].ToString(), new UserMusicInfo());
                    //m_drumMusicInfos.Add(musiclist[i]["MusicName"].ToString(), new UserMusicInfo());
                    //m_guitarMusicInfos.Add(musiclist[i]["MusicName"].ToString(), new UserMusicInfo());
                    //m_vocalMusicInfos.Add(musiclist[i]["MusicName"].ToString(), new UserMusicInfo());
                    //m_keyboardMusicInfos.Add(musiclist[i]["MusicName"].ToString(), new UserMusicInfo());
                }
            }

        }

        public void SetBackGroundData(List<Dictionary<string, object>> backgroundList)
        {
            for(int i = 0; i < backgroundList.Count;i++)
            {
                m_backGroundInfos.Add(backgroundList[i]["FileName"].ToString(), new BackGroundInfo());
            }
        }

        public void SetStoryInfoData(List<Dictionary<string,  object>> storyInfoList)
        {
            for(int i = 0; i<storyInfoList.Count;i++)
            {
                        m_storyInfos.Add(storyInfoList[i]["StoryName"].ToString(), new StoryInfo());   
            }

            //foreach(KeyValuePair<string, StoryInfo> item in m_storyInfos)
            //{
            //    Debug.Log("메인 스토리 : " + item.Key + " : " + m_storyInfos[item.Key].m_isOpen);
            //}
        }

        public UserMusicInfo GetUserMusicInfo(string musicName, MODE mode)
        {
            //switch(mode)
            //{
            //    case MODE.BASE:
            //        return m_vocalMusicInfos[musicName];
                    
            //    case MODE.GUITAR:
            //        return m_guitarMusicInfos[musicName];
                    
            //    case MODE.DRUM:

            //        return m_drumMusicInfos[musicName];
                    
            //    case MODE.PIANO:
            //        return m_keyboardMusicInfos[musicName];
            //}

            return m_musicInfos[(int)mode - 1][musicName];
        }
    }

    [System.Serializable]
    public class UserMusicInfo
    {
        public bool[] m_isClaer;
        public int[] m_combo;
        public int[] m_score;
        public int[] m_perfect;
        public int[] m_great;
        public int[] m_miss;
        public int[] m_bad;

        public UserMusicInfo()
        {
            m_isClaer = new bool[3];
            m_combo = new int[3];
            m_score = new int[3];
            m_perfect = new int[3];
            m_great = new int[3];
            m_miss = new int[3];
            m_bad = new int[3];

            for (int i = 0; i < 3; i++)
            {
                m_isClaer[i] = false;
                m_combo[i] = 0;
                m_score[i] = 0;
                m_perfect[i] = 0;
                m_great[i] = 0;
                m_miss[i] = 0;
                m_bad[i] = 0;

                //m_combo[i] = Random.Range(0, 10000);
                //m_score[i] = Random.Range(0, 10000);
                //m_perfect[i] = Random.Range(0, 10000);
                //m_great[i] = Random.Range(0, 10000);
                //m_miss[i] = Random.Range(0, 10000);
                //m_bad[i] = Random.Range(0, 10000);
            }
        }


        public void SetMusicInfo(Difficulty difficulty, bool isClear, int combo, int score, int perfect, int great, int miss, int bad)
        {
            m_isClaer[(int)difficulty - 1] = isClear;
            m_combo[(int)difficulty - 1] = combo;
            m_score[(int)difficulty - 1] = score;
            m_perfect[(int)difficulty - 1] = perfect;
            m_great[(int)difficulty - 1] = great;
            m_miss[(int)difficulty - 1] = miss;
            m_bad[(int)difficulty - 1] = bad;
        }

        public void SetMusicInfo(Difficulty difficulty, bool isClear)
        {
            m_isClaer[(int)difficulty - 1] = isClear;
        }

        public void SetMusicInfo(Difficulty difficulty, int combo, int score)
        {
            m_combo[(int)difficulty - 1] = combo;
            m_score[(int)difficulty - 1] = score;
        }

        public void SetMusicInfo(Difficulty difficulty, int perfect, int great, int miss, int bad)
        {
            m_perfect[(int)difficulty - 1] = perfect;
            m_great[(int)difficulty - 1] = great;
            m_miss[(int)difficulty - 1] = miss;
            m_bad[(int)difficulty - 1] = bad;
        }

        public bool GetIsClear(Difficulty difficulty)
        {
            return m_isClaer[(int)difficulty - 1];
        }

        public int GetScore(Difficulty difficulty)
        {
            return m_score[(int)difficulty - 1];
        }

        public int GetCombo(Difficulty difficulty)
        {
            return m_combo[(int)difficulty - 1];
        }

        public int GetPerfect(Difficulty difficulty)
        {
            return m_perfect[(int)difficulty - 1];
        }

        public int GetGreat(Difficulty difficulty)
        {
            return m_great[(int)difficulty - 1];
        }

        public int GetMiss(Difficulty difficulty)
        {
            return m_miss[(int)difficulty - 1];
        }

        public int GetBad(Difficulty difficulty)
        {
            return m_bad[(int)difficulty - 1];
        }
    }

    [System.Serializable]
    public class BackGroundInfo
    {
        public bool m_isOpen = true;

        public void SetisOpen(bool isOpen)
        {
            m_isOpen = isOpen;
        }
    }

    [System.Serializable]
    public class StoryInfo
    {
        public bool m_isOpen ;
        public bool m_isClear ;

        public void SetisOpen(bool isOpen)
        {
            m_isOpen = isOpen;
        }

        public void SetisClear(bool isClear)
        {
            m_isClear = isClear;
        }
    }


    [System.Serializable]
    public class QuestInfo
    {
        public bool m_isClear = true;

        public void SetIsClear(bool isClear)
        {
            m_isClear = isClear;
        }
    }

}