using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace TH
{


    public class UIShowToggle : UIButton
    {
        public bool m_toggle;
        public GameObject[] m_toggleOnObject;
        public GameObject[] m_toggleOffObject;

        public void OnEnable()
        {
            TH.GameInfoManager.Instance.m_gameInfo.m_helpnum = 0;
        }


        public void AddOnArray(GameObject obj)
        {
            GameObject[] temp = new GameObject[m_toggleOnObject.Length + 1];

            for (int i = 0; i < m_toggleOffObject.Length; i++)
            {
                temp[i] = m_toggleOnObject[i];
            }

            temp[temp.Length - 1] = obj;

            m_toggleOnObject = temp;
        }

        public void AddOffArray(GameObject obj)
        {
            GameObject[] temp = new GameObject[m_toggleOffObject.Length + 1];

            for(int i =0; i<m_toggleOffObject.Length;i++)
            {
                temp[i] = m_toggleOffObject[i];
            }

            temp[temp.Length - 1] = obj;

            m_toggleOffObject = temp;
        }

        public override void Click()
        {

            if (m_toggle)
            {
             
                for (int i = 0; i < m_toggleOnObject.Length; i++)
                {
                    if(m_toggleOnObject[i])
                        m_toggleOnObject[i].SetActive(true);
                }

                for (int i = 0; i < m_toggleOffObject.Length; i++)
                {
                    if (m_toggleOffObject[i])
                        m_toggleOffObject[i].SetActive(false);
                }
               
            }
            else
            {
              
                for (int i = 0; i < m_toggleOnObject.Length; i++)
                {
                    if (m_toggleOnObject[i])
                        m_toggleOnObject[i].SetActive(false);
                }

                for (int i = 0; i < m_toggleOffObject.Length; i++)
                {
                    if (m_toggleOffObject[i])
                        m_toggleOffObject[i].SetActive(true);
                }
               
            }

            m_toggle = !m_toggle;
        }
    }
   
}