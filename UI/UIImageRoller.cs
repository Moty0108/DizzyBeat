using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImageRoller : MonoBehaviour
{
    public Image[] m_textImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        for (int i = 0; i<m_textImage.Length;i++)
        {
            m_textImage[i].transform.position += Vector3.left * Time.deltaTime * 100;
            if(m_textImage[i].GetComponent<RectTransform>().anchoredPosition.x < -m_textImage[i].GetComponent<RectTransform>().sizeDelta.x)
            {
                m_textImage[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(m_textImage[i].GetComponent<RectTransform>().sizeDelta.x, m_textImage[i].GetComponent<RectTransform>().anchoredPosition.y);
            }
        }
    }
}
