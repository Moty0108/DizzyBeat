using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH
{

    public class UIBtnSelectMode : UIButton
    {
        public MODE m_mode;
        public GameObject m_helper;
        private void OnDisable()
        {
            m_helper.SetActive(true);
        }
        public void OnEnable()
        {
            TH.GameInfoManager.Instance.m_gameInfo.m_helpnum = -1;
            m_helper.SetActive(false);
        }
        public override void Click()
        {
            GameInfoManager.Instance.SetMode(m_mode);
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