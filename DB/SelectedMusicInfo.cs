using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TH
{


    public class SelectedMusicInfo : MonoBehaviour
    {
        public string m_musicName;

        public Text m_difficultyText;
        public Image m_isClearImage;
        public Text m_musicTitle;
        public Text m_artistText;
        public Text m_comboText;
        public Text m_scoreText;
        public GameObject[] m_starts;

        // Start is called before the first frame update
        void Awake()
        {
            
        }

        private void OnEnable()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetData(string musicTitle, string artist, int combo, bool isClear, int score)
        {
            for (int i = 0; i < m_starts.Length; i++)
            {
                m_starts[i].SetActive(false);
            }

            m_musicName = musicTitle;
            m_musicTitle.text = musicTitle;
            m_artistText.text = artist;
            m_comboText.text = combo.ToString();
            m_scoreText.text = score.ToString();

            m_isClearImage.gameObject.SetActive(isClear);
        }

        public void SetData(string musicTitle, string artist, int combo, bool isClear, int score, int level)
        {
            for (int i = 0; i < m_starts.Length; i++)
            {
                m_starts[i].SetActive(false);
            }

            m_musicName = musicTitle;
            m_musicTitle.text = musicTitle;
            m_artistText.text = artist;
            m_comboText.text = combo.ToString();
            m_scoreText.text = score.ToString();

            m_isClearImage.gameObject.SetActive(isClear);

            for(int i = 0; i < level; i++)
            {
                m_starts[i].SetActive(true);
            }
        }

        public void SetData(int combo, bool isClear, int score, int level)
        {
            for (int i = 0; i < m_starts.Length; i++)
            {
                m_starts[i].SetActive(false);
            }

            m_comboText.text = combo.ToString();
            m_scoreText.text = score.ToString();

            for (int i = 0; i < level; i++)
            {
                m_starts[i].SetActive(true);
            }

            m_isClearImage.gameObject.SetActive(isClear);
        }
    }
}