using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH
{
    public enum AudioType
    {
        BACKGROUND = 0, EFFECT, END
    }

    public class SoundManager : Singleton<SoundManager>
    {
        AudioSource[] m_audioSources;
        public AudioClip m_backGroundMusic;

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

            m_audioSources = new AudioSource[(int)AudioType.END];

            for(int i = 0; i < (int)AudioType.END; i++)
            {
                m_audioSources[i] = gameObject.AddComponent<AudioSource>();
            }

            
            if (m_backGroundMusic)
            {
                m_audioSources[(int)AudioType.BACKGROUND].clip = m_backGroundMusic;
                m_audioSources[(int)AudioType.BACKGROUND].Play();
                m_audioSources[(int)AudioType.BACKGROUND].loop = true;
            }
            
        }
        /// <summary>
        /// 오디오 클립을 설정
        /// 
        /// </summary>
        /// <param name="type">설정할 오디오 타입</param>
        /// <param name="clip">설정할 오디오 클립</param>
        public void SetClip(AudioType type, AudioClip clip)
        {
            m_audioSources[(int)type].clip = clip;
        }

        public void SetAudipVolume(AudioType type, float value)
        {
            m_audioSources[(int)type].volume = value;
        }
      

        public void SetActive(AudioType type, bool value)
        {
            m_audioSources[(int)type].enabled = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">반환할 오디오 타입</param>
        /// <returns>오디오 소스</returns>
        public AudioSource GetAudipSource(AudioType type)
        {
            return m_audioSources[(int)type];
        }

        /// <summary>
        /// 재생중인 오디오를 스탑
        /// </summary>
        /// <param name="type">멈출 오디오 타입</param>
        public void StopAudio(AudioType type)
        {
            m_audioSources[(int)type].Stop();
        }
        public void PauseAudio(AudioType type)
        {
            m_audioSources[(int)type].Pause();
        }
        public void Replay(AudioType type)
        {
            m_audioSources[(int)type].UnPause();
        }

        /// <summary>
        /// 예전에 재생중이던 오디오를 끄고
        /// 클립에 있는 오디오를 재생함
        /// 이 함수 호출전에 SetClip을 호출하여 AudioClip을 바꿔줘야함
        /// </summary>
        /// <param name="type">재생할 오디오 타입</param>
        public void PlayAudio(AudioType type)
        {
            m_audioSources[(int)type].Play();
        }

        /// <summary>
        /// 이 함수는 예전에 재생중이던 오디오가 있더라도
        /// 그 오디오는 계속 재생중이고 추가로 이 함수의 파라미터로 넘어온 클립을 재생함
        /// </summary>
        /// <param name="type">재생할 오디오 타입</param>
        /// <param name="clip">재상할 오디오 클립</param>
        public void PlayAudio(AudioType type, AudioClip clip)
        {
            m_audioSources[(int)type].PlayOneShot(clip);
        }

        //public void SetBackGroundVolume(float value)
        //{
        //    m_audioSources[(int)AudioType.BACKGROUND].volume = value;
        //}

        //public void SetEffectVolume(float value)
        //{
        //    m_audioSources[(int)AudioType.EFFECT].volume = value;
        //}

        //public void PlayBackGroundAudio(AudioClip clip)
        //{
        //    m_audioSources[(int)AudioType.BACKGROUND].PlayOneShot(clip);
        //}

        //public void PlayEffectAudio(AudioClip clip)
        //{
        //    m_audioSources[(int)AudioType.EFFECT].PlayOneShot(clip);
        //}
    }

}