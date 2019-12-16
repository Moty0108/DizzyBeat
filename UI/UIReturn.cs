using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH
{
   
    public class UIReturn : UIButton
    {
        public AudioClip m_audio;

        public override void Click()
        {
            //GetComponentInParent<UIPanel>().Return();
            GetComponentInParent<UIPanel>().Return();


        }
        public void ReturnMusic()
        {
            if (TH.SoundManager.Instance.GetAudipSource(AudioType.BACKGROUND).clip != m_audio)
            {
                TH.SoundManager.Instance.StopAudio(AudioType.BACKGROUND);
                TH.SoundManager.Instance.SetClip(AudioType.BACKGROUND, m_audio);
                TH.SoundManager.Instance.PlayAudio(AudioType.BACKGROUND);
            }

        }
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}