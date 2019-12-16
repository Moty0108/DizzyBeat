using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


namespace TH
{
    public enum Difficulty
    {
        NONE, EASY, MEDIUM, HARD
    }

    public enum MODE
    {
        NONE, GUITAR, VOCAL, KEYBOARD, DRUM
    }

    public enum GameSpeed
    {
        One = 1, Two = 2, Four = 3, Eight = 4
    }

    [System.Serializable]
    public class GameInfo
    {
        public string m_songName;
        public MODE m_mode;
        public int m_speed;
        public GameSpeed m_gamespeed;
        public Difficulty m_difficulty;
        public string m_songCSV;
        public string m_questCSV;
        public bool m_isStory;
        public int m_skin;
        public int[] m_levels;
        public string m_artistName;
        public string m_storyName;

        public bool isSeletSong;
        public int m_helpnum;
        public GameInfo()
        {
   
            m_songName = "";
            m_mode = MODE.GUITAR;
            m_gamespeed = GameSpeed.One;
            m_speed = 15;
            m_difficulty = Difficulty.EASY;
            m_songCSV = "";
            m_questCSV = null;
            m_isStory = false;
            m_skin = 0;
            m_artistName = "";
            isSeletSong = false;
            m_helpnum = 0;
            m_storyName = "";


        }
    }

    public class GameInfoManager : Singleton<GameInfoManager>
    {
        public GameInfo m_gameInfo;

        public string m_storyCSV;

        // Start is called before the first frame update
        void Awake()
        {

             //Debug.unityLogger.logEnabled = false;
            //int i_width = Screen.width;
            //int i_height = Screen.height;
            
            //Screen.SetResolution(i_width, i_height, true);



            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);


            m_gameInfo = new GameInfo();
            Debug.Log(this.ToString() + " 싱글톤 객체 초기화");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public GameInfo GetGameInfo()
        {
            return m_gameInfo;
        }

        public void SetQuestCSV(string csv)
        {
            m_gameInfo.m_questCSV = csv;
        }

        public string GetQuestCSV()
        {
            return m_gameInfo.m_questCSV;
        }

        public void SetStoryCSV(string csv)
        {
            m_storyCSV = csv;
        }

        public string GetStoryCSV()
        {
            return m_storyCSV;
        }

        public void SetMusic(string musicName)
        {
            m_gameInfo.m_songName = musicName;
        }

        public void SetMusicCSV(string csvName)
        {
            m_gameInfo.m_songCSV = csvName;
        }

        public void SetMode(MODE mode)
        {
            Debug.Log("모드 : " + mode.ToString() + "선택");
            m_gameInfo.m_mode = mode;
        }

        public void SetDifficulty(Difficulty difficulty)
        {
            Debug.Log("난이도 : " + difficulty.ToString() + "선택!");
            m_gameInfo.m_difficulty = difficulty;
        }

        public void SetSpeed(int speed)
        {
            m_gameInfo.m_speed = speed;
        }
        public void setArtist(string name)
        {
            m_gameInfo.m_artistName = name;
        }
       
    }
}