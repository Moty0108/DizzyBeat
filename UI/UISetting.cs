using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TH
{
    public class UISetting : MonoBehaviour
    {
        public Slider m_backGroundSlider;
        public Slider m_effectSlider;
        public Slider m_sinkSlider;

        public Toggle m_backGroundToggle;
        public Toggle m_effectToggle;
        public Toggle m_vibrationToggle;
        public Text m_sinkText;

        // Start is called before the first frame update
        void Start()
        {
            m_backGroundSlider.onValueChanged.AddListener(SetBackGroundVolume);
            m_backGroundSlider.value = SettingManager.Instance.m_settingValue.backGroundVolume;

            m_effectSlider.onValueChanged.AddListener(SetEffectVolume);
            m_effectSlider.value = SettingManager.Instance.m_settingValue.effectVolume;

            m_backGroundToggle.onValueChanged.AddListener(SetBackGroundToggle);
            m_backGroundToggle.isOn = SettingManager.Instance.m_settingValue.isBackGround;

            m_effectToggle.onValueChanged.AddListener(SetEffectToggle);
            m_effectToggle.isOn = SettingManager.Instance.m_settingValue.isEffect;
        

            m_vibrationToggle.onValueChanged.AddListener(SetVibrationToggle);
            m_vibrationToggle.isOn = SettingManager.Instance.m_settingValue.isVibration;

            m_sinkSlider.onValueChanged.AddListener(SetSink);
            m_sinkSlider.value = SettingManager.Instance.m_settingValue.m_sink;
        }


        public void SetBackGroundVolume(float value)
        {
            SettingManager.Instance.m_settingValue.backGroundVolume = value;
            SoundManager.Instance.SetAudipVolume(AudioType.BACKGROUND, value);
        }

        public void SetEffectVolume(float value)
        {
            SettingManager.Instance.m_settingValue.effectVolume = value;
            SoundManager.Instance.SetAudipVolume(AudioType.EFFECT, value/5);
        }

        public void SetSink(float value)
        {
            SettingManager.Instance.m_settingValue.m_sink = value;
            if(value>0)
                SettingManager.Instance.m_settingValue.m_sinkText = "+"+value.ToString();
            else if(value<=0)
                  SettingManager.Instance.m_settingValue.m_sinkText = value.ToString();
    
            m_sinkText.text = SettingManager.Instance.m_settingValue.m_sinkText;
        }

        public void SetBackGroundToggle(bool value)
        {
            SettingManager.Instance.m_settingValue.isBackGround = value;
            SoundManager.Instance.SetActive(AudioType.BACKGROUND, value);

            if (value)
            {
                m_backGroundSlider.value = SettingManager.Instance.m_settingValue.backGroundPrevVolume;
            }
            else
            {
                SettingManager.Instance.m_settingValue.backGroundPrevVolume = m_backGroundSlider.value;
                m_backGroundSlider.value = 0;
            }
        }

        public void SetEffectToggle(bool value)
        {
            SettingManager.Instance.m_settingValue.isEffect = value;
            SoundManager.Instance.SetActive(AudioType.EFFECT, value);
           
            if (value)
            {
                m_effectSlider.value = SettingManager.Instance.m_settingValue.effectPrevVolume;
            }
            else
            {
                SettingManager.Instance.m_settingValue.effectPrevVolume = m_effectSlider.value;
                m_effectSlider.value = 0;
            }
            SettingManager.Instance.m_settingValue.isEffect = value;


        }





        public void SetVibrationToggle(bool value)
        {
            SettingManager.Instance.m_settingValue.isVibration = value;
        }
    }

}
