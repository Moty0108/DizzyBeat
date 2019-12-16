using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH
{

    public class UIOpenPanel : UIButton
    {
        public GameObject[] m_onTargets;
        public GameObject[] m_offTargets;
    
        
        public override void Click()
        {
            GetComponentInParent<UIPanel>().OpenPanel(m_onTargets, m_offTargets);
      
        }

    }
}