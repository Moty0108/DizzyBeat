using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH
{
    public class UIBtnSetStoryMode : UIButton
    {
        public bool m_isStory;

        public override void Click()
        {
            GameInfoManager.Instance.m_gameInfo.m_isStory = m_isStory;
        }
        private void OnEnable()
        {
            TH.GameInfoManager.Instance.m_gameInfo.m_helpnum = 1;
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