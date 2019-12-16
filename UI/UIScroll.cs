using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TH
{

    public class UIScroll : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public RectTransform m_content;

        bool m_clicked = false;
        float m_power = 0;
        float m_prevPosition;

        public int m_selectedNumber;
        public Transform[] objs;
        Vector3[] m_offset;

        bool m_isScroll = false;
        bool m_upDown;
        // Start is called before the first frame update
        void Start()
        {
            //objs = GetComponentsInChildren<Transform>();
            m_offset = new Vector3[objs.Length];


            for (int i = 0; i < objs.Length; i++)
            {
                m_offset[i] = objs[i].transform.localPosition;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (m_power > 0.1f)
            {
                m_power -= Time.deltaTime;
            }
            else if (m_power < -0.1f)
            {
                m_power += Time.deltaTime;
            }
            else
            {
                m_power = 0;
            }

            //if (m_power != 0)
            //    m_content.transform.localPosition += new Vector3(0, m_power, 0);

            if (!m_isScroll && m_clicked && m_power != 0)
            {
                StartCoroutine(MovePannel());
            }


        }

        public void UpPannel()
        {
            for (int i = 0; i < objs.Length - 1; i++)
            {
                objs[i].transform.localPosition = objs[i + 1].transform.localPosition;
            }
            objs[objs.Length - 1].transform.localPosition = objs[0].transform.localPosition;
        }

        public void DownPannel()
        {

        }

        IEnumerator MovePannel()
        {
            m_isScroll = true;

            float time = 0;

            while (true)
            {
                time += Time.deltaTime * Mathf.Max(Mathf.Abs(m_power), 1);


                if (m_upDown)
                {
                    for (int i = 0; i < objs.Length; i++)
                    {
                        objs[i].transform.localPosition = Vector2.Lerp(m_offset[i], m_offset[(i + 1) % m_offset.Length], time);
                    }
                }
                else
                {
                    for (int i = 0; i < objs.Length; i++)
                    {
                        objs[(i + 1) % objs.Length].transform.localPosition = Vector2.Lerp(m_offset[(i + 1) % m_offset.Length], m_offset[i], time);
                    }
                }


                if (time > 1)
                    break;

                yield return null;
            }

            m_selectedNumber = (m_selectedNumber + 1) % objs.Length;

            for (int i = 0; i < objs.Length; i++)
            {
                m_offset[i] = objs[i].transform.localPosition;
            }

            if (m_power != 0)
            {
                Debug.Log("aa");
                StartCoroutine(MovePannel());
            }
            else
            {
                m_isScroll = false;
            }
        }

        public int Return(float num)
        {
            if (num > 0)
                return 1;
            else if (num < 0)
                return -1;
            else
                return 0;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Down");
            m_power = 0;
            m_clicked = true;
            m_prevPosition = eventData.position.y;
        }

        public void OnDrag(PointerEventData eventData)
        {

            if (eventData.position.y < m_prevPosition)
            {
                m_power = (m_prevPosition - eventData.position.y);
            }
            else if (eventData.position.y > m_prevPosition)
            {
                m_power = (m_prevPosition - eventData.position.y);
            }

            if (m_power > 0)
            {
                m_upDown = true;
            }
            else
            {
                m_upDown = false;
            }

            m_prevPosition = eventData.position.y;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_clicked = false;
            Debug.Log("UP");

            //StartCoroutine(MovePannel());
        }
    }
}