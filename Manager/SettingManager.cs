using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TH
{
    [System.Serializable]
    public class SettingValue
    {
        public float backGroundVolume = 1f;
        public float backGroundPrevVolume = 1f;
        public bool isBackGround = true;
        public float effectVolume = 5f;
        public float effectPrevVolume = 5f;
        public bool isEffect = true;
        public bool isVibration = false;

        public float m_sink = 0;
        public string m_sinkText = "";
    }

    public class SettingManager : Singleton<SettingManager>
    {
        public SettingValue m_settingValue;
 
        void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);

            m_settingValue = new SettingValue();
        }

 
    }

}