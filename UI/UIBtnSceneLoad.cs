using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TH
{

    public class UIBtnSceneLoad : UIButton
    {
        public string m_sceneName;
        public bool m_isStory;
        public override void Click()
        {
            if (m_isStory)
            {
                //SceneManager.LoadScene("Story");
                LoadingSceneManager.SceneLoad("Story");
                return;
            }

            switch (GameInfoManager.Instance.m_gameInfo.m_mode)
            {
                case MODE.DRUM:
                    //SceneManager.LoadScene("Type3");
                    LoadingSceneManager.SceneLoad("Type3");
                    break;

                case MODE.GUITAR:
                    //SceneManager.LoadScene("Type2");
                    LoadingSceneManager.SceneLoad("Type2");
                    break;

                case MODE.KEYBOARD:
                    //SceneManager.LoadScene("Type4");
                    LoadingSceneManager.SceneLoad("Type4");
                    break;

                case MODE.VOCAL:
                    //SceneManager.LoadScene("Type1");
                    LoadingSceneManager.SceneLoad("Type1");
                    break;
            }
            
        }

    }

}