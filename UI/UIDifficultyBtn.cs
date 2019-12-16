using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TH
{
    public class UIDifficultyBtn : UIButton
    {
        public Difficulty m_difficulty = Difficulty.NONE;
        public Image m_infoBackGroundImage;
        public Image m_startBtnImage;
        public Image m_levelTextImage;
        public Sprite m_infoBackGroundSprite;
        public Sprite m_startBtnSprite;
        public Sprite m_levelTextSprite;
        public SelectedMusicInfo m_selectedinfo;
        public override void Click()
        {
            GameInfoManager.Instance.SetDifficulty(m_difficulty);

            m_infoBackGroundImage.sprite = m_infoBackGroundSprite;
            m_startBtnImage.sprite = m_startBtnSprite;

            Material temp = new Material(m_levelTextImage.material.shader);
            temp.mainTexture = m_levelTextSprite.texture;
            m_levelTextImage.material = temp;
            m_selectedinfo.SetData(UserInfoDBManager.Instance.m_user.GetUserMusicInfo(m_selectedinfo.m_musicName, GameInfoManager.Instance.m_gameInfo.m_mode).GetCombo(m_difficulty), UserInfoDBManager.Instance.m_user.GetUserMusicInfo(m_selectedinfo.m_musicName, GameInfoManager.Instance.m_gameInfo.m_mode).GetIsClear(m_difficulty), UserInfoDBManager.Instance.m_user.GetUserMusicInfo(m_selectedinfo.m_musicName, GameInfoManager.Instance.m_gameInfo.m_mode).GetScore(m_difficulty), GameInfoManager.Instance.m_gameInfo.m_levels[(int)m_difficulty - 1]);

            for (int i = 0; i < DBManager.Instance.GetMusicDataLength(); i++)
            {
                if (DBManager.Instance.GetMusicDataObject(i, "MusicName").ToString() == GameInfoManager.Instance.m_gameInfo.m_songName)
                {
                    switch (m_difficulty)
                    {
                        case Difficulty.EASY:
                            switch (GameInfoManager.Instance.m_gameInfo.m_mode)
                            {
                                case MODE.VOCAL:
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(i, "EASY").ToString());
                                    break;

                                case MODE.GUITAR:
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(i, "GUITAREASY").ToString());
                                    break;

                                case MODE.DRUM:
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(i, "DRUMEASY").ToString());
                                    break;

                                case MODE.KEYBOARD:
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(i, "KEYBOARDEASY").ToString());
                                    break;
                            }
                            break;
                        case Difficulty.MEDIUM:
                            switch (GameInfoManager.Instance.m_gameInfo.m_mode)
                            {
                                case MODE.VOCAL:
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(i, "MEDIUM").ToString());
                                    break;                                                                     
                                                                                                               
                                case MODE.GUITAR:                                                              
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(i, "GUITARMEDIUM").ToString());
                                    break;                                                                    
                                                                                                              
                                case MODE.DRUM:                                                               
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(i, "DRUMMEDIUM").ToString());
                                    break;                                                                     
                                                                                                               
                                case MODE.KEYBOARD:                                                            
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(i, "KEYBOARDMEDIUM").ToString());
                                    break;
                            }
                            break;
                        case Difficulty.HARD:
                            switch (GameInfoManager.Instance.m_gameInfo.m_mode)
                            {
                                case MODE.VOCAL:
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(i, "HARD").ToString());
                                    break;

                                case MODE.GUITAR:
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(i, "GUITARHARD").ToString());
                                    break;

                                case MODE.DRUM:
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(i, "DRUMHARD").ToString());
                                    break;

                                case MODE.KEYBOARD:
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(i, "KEYBOARDHARD").ToString());
                                    break;
                            }
                            break;
                    }
                    break;
                }
            }

            //GameInfoManager.Instance.SetMusicCSV();
        }

    }
}