using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH
{
    public class DBManager : Singleton<DBManager>
    {
        List<Dictionary<string, object>> m_storyData;
        List<Dictionary<string, object>> m_questData;
        List<Dictionary<string, object>> m_backGroundData;
        List<Dictionary<string, object>> m_musicData;

        public int random;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            instance = this;
            DontDestroyOnLoad(gameObject);
            

            m_storyData = CSVReader.Read("StoryTable");
            m_questData = CSVReader.Read("QuestTable");
            m_backGroundData = CSVReader.Read("BackGroundTable");
            m_musicData = CSVReader.Read("SelectSongMusicTable");

            Debug.Log(this.ToString() + " 싱글톤 객체 초기화");
        }

        public List<Dictionary<string, object>> GetStoryData()
        {
            return m_storyData;
        }

        public object GetStoryDataObject(int index, string name)
        {
            return m_musicData[index][name];
        }

        public int GetStoryDataLength()
        {
            return m_musicData.Count;
        }

        public List<Dictionary<string, object>> GetMusicData()
        {
            return m_musicData;
        }

        public object GetMusicDataObject(int index, string name)
        {
            return m_musicData[index][name];
        }

        public int GetMusicDataLength()
        {
            return m_musicData.Count;
        }

        public List<Dictionary<string, object>> GetQuestData()
        {
            return m_questData;
        }

        public object GetQuestDataObject(int index, string name)
        {
            return m_musicData[index][name];
        }

        public int GetQuestDataLength()
        {
            return m_musicData.Count;
        }

        public List<Dictionary<string, object>> GetBackGroundData()
        {
            return m_backGroundData;
        }

        public object GetBackGroundDataObject(int index, string name)
        {
            return m_musicData[index][name];
        }

        public int GetBackGroundDataLength()
        {
            return m_musicData.Count;
        }

    }
}