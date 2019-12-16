using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace TH
{
    public class MusicInfo : MonoBehaviour
    {
        public string m_name;
        public string m_artist;
        public int m_combo;
        public int m_score;
        public int[] m_levels;

        public Text m_nameText;
        public Text m_artistText;
        public Text m_comboText;
        public Text m_scoreText;

        // Start is called before the first frame update
        void Start()
        {
        }

        public void Set()
        {
            //m_nameText.text = m_name;
            //m_artistText.text = m_artist;
            //m_comboText.text = m_combo.ToString();
            //m_scoreText.text = m_score.ToString();
        }

        public void SetInfo(string name, string artist, int combo, int score, string level)
        {
            m_name = name;
            m_artist = artist;
            m_combo = combo;
            m_score = score;

            m_levels = new int[3];

            string[] temp = level.Split('|');
            
            for(int i = 0; i < 3;i++)
            {
                m_levels[i] = int.Parse(temp[i]);
            }

            Set();
        }

        public void SetName(string name)
        {
            m_name = name;

            Set();
        }

        public void SetArtist(string artist)
        {
            m_artist = artist;

            Set();
        }

        public void SetCombo(int combo)
        {
            m_combo = combo;

            Set();
        }

        public void SetScore(int score)
        {
            m_score = score;

            Set();
        }
    }
}