using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH
{

    public class UISpeedBtn : UIButton
    {
        public int m_speed;
        public void OnEnable()
        {
            TH.GameInfoManager.Instance.m_gameInfo.m_helpnum = 3;
        }
        public override void Click()
        {
            GameInfoManager.Instance.SetSpeed(m_speed);
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