using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TH
{
    public class UIRadioButtonObject : MonoBehaviour
    {
        public Sprite m_selectSprite;
        public Sprite m_nonSelectSprite;

        public Image m_image;

        private void Awake()
        {
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Select()
        {
            m_image.sprite = m_selectSprite;
        }

        public void NonSelect()
        {
            m_image.sprite = m_nonSelectSprite;
        }
    }

}