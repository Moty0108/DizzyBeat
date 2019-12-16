using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH
{

    public class UIPanel : MonoBehaviour
    {
        public GameObject m_mainPanel;

        Stack<GameObject[]> m_onObject;
        Stack<GameObject[]> m_offObject;

        private void Awake()
        {
            m_onObject = new Stack<GameObject[]>();
            m_offObject = new Stack<GameObject[]>();
        }

        public void OpenPanel(GameObject[] on, GameObject[] off)
        {
            for (int i = 0; i < on.Length; i++)
            {
                on[i].SetActive(true);
            }

            for (int i = 0; i < off.Length; i++)
            {
                off[i].SetActive(false);
            }

            m_onObject.Push(on);
            m_offObject.Push(off);
        }


        public void Return()
        {
            GameObject[] temp = m_onObject.Pop();

            for (int i = 0; i < temp.Length; i++)
            {
                temp[i].SetActive(false);
            }

            temp = m_offObject.Pop();

            for (int i = 0; i < temp.Length; i++)
            {
                temp[i].SetActive(true);
            }

        }
    }
}