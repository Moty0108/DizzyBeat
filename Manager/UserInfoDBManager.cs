using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

namespace TH
{
    public class UserInfoDBManager : Singleton<UserInfoDBManager>
    {
        public UserInfo m_user;
        public string m_fileName = "";

        public string temp;
        public int m_startSkin;
        
        public List<DataUpdateObserver> observer;
        public List<DataInitObserver> m_initObserver;

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

            //m_user = new UserInfo("aaaa", DBManager.Instance.GetMusicData());
            //m_user.SetBackGroundData(DBManager.Instance.GetBackGroundData());
            //m_user.SetStoryInfoData(DBManager.Instance.GetStoryData());


            //Save(m_user);
            m_user = Load();
            Debug.Log(this.ToString() + " 싱글톤 객체 초기화");


            SetInitObserver();
            SetUpdateObserver();
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void SetInitObserver()
        {
            var temp = FindObjectsOfTypeAll(typeof(MonoBehaviour)).OfType<DataInitObserver>();

            m_initObserver = new List<DataInitObserver>();

            string result = "";

            foreach (DataInitObserver i in temp)
            {
                result += i.ToString() + ", ";
                m_initObserver.Add(i);
                i.InitData();
            }

            Debug.Log("InitObserver Count : " + temp.Count<DataInitObserver>() + " : " + result);
        }

        public void SetUpdateObserver()
        {
            var temp = FindObjectsOfTypeAll(typeof(MonoBehaviour)).OfType<DataUpdateObserver>();

            observer = new List<DataUpdateObserver>();

            string result = "";

            foreach (DataUpdateObserver i in temp)
            {
                result += i.ToString() + ", ";
                observer.Add(i);
            }

            Debug.Log("UpdateObserver Count : " + temp.Count<DataUpdateObserver>() + " : " + result);
        }

        private void OnLevelWasLoaded(int level)
        {
            SetInitObserver();
            SetUpdateObserver();
            UpdateData();
            if(SkinManager.Instance)
                SkinManager.Instance.SetSkin(GameInfoManager.Instance.m_gameInfo.m_skin);
        }


        public void UpdateData()
        {
            for(int i = 0; i<observer.Count;i++)
            {
                Debug.Log(observer[i]);
                observer[i].UpdateData();
            }
        }

        public void UpdateMusicData(string musicName, MODE mode, Difficulty difficulty, bool isClear, int combo, int score, int perfect, int great, int bad, int miss)
        {
            m_user.GetUserMusicInfo(musicName, mode).SetMusicInfo(difficulty, isClear, combo, score, perfect, great, miss, bad);
        }

        [ContextMenu("Test")]
        public void ResultTest()
        {
            m_user.m_backGroundInfos[temp].SetisOpen(true);
            //m_user.m_storyInfos[temp].m_isClear = true;
            UpdateData();
        }

        [ContextMenu("aa")]
        public void aa()
        {
            SetStoryIsClear(temp, true);
            Save(m_user);
        }

        

        public void SetStoryIsClear(string storyName, bool isClear)
        {
            // 에외 처리하기
            m_user.m_storyInfos[storyName].m_isClear = isClear;

            if (isClear)
            {
                for (int i = 0; i < DBManager.Instance.GetStoryData().Count; i++)
                {
                    if (storyName == DBManager.Instance.GetStoryData()[i]["StoryName"].ToString())
                    {
                        m_user.m_storyInfos[DBManager.instance.GetStoryData()[i + 1]["StoryName"].ToString()].m_isOpen = true;
                        if (DBManager.Instance.GetStoryData()[i + 1]["StoryType"].ToString() == "MAIN")
                        {
                            m_user.m_storyInfos[DBManager.instance.GetStoryData()[i + 2]["StoryName"].ToString()].m_isOpen = true;
                        }
                    }
                }
            }

            UpdateData();
        }

        public void SetStoryIsOpen(string storyName, bool isOpen)
        {
            m_user.m_storyInfos[storyName].m_isOpen = isOpen;
            
        }

        public void Save(UserInfo info)
        {
            BinarySerialize(info, Application.persistentDataPath + "/"  + m_fileName + ".bin");
        }

        public UserInfo Load()
        {
            UserInfo temp;
            try
            {
                temp = BinaryDeserialize<UserInfo>(Application.persistentDataPath + "/" + m_fileName + ".bin");
            }
            catch (FileNotFoundException e)
            {
                Debug.Log(e);
                Debug.Log("유저 정보 없음!");
                temp = new UserInfo("aaaa", DBManager.Instance.GetMusicData());
                temp.SetBackGroundData(DBManager.Instance.GetBackGroundData());
                temp.SetStoryInfoData(DBManager.Instance.GetStoryData());
                Save(temp);
                
            }

            return temp;
        }

        public void BinarySerialize<T>(T info, string filePath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Create);
            formatter.Serialize(stream, info);
            stream.Close();
        }

        public T BinaryDeserialize<T>(string filePath)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);
            T info = (T)formatter.Deserialize(stream);
            stream.Close();
            return info;
        }
    }
}