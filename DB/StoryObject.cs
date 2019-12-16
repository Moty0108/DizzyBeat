using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TH
{
    public class StoryObject : MonoBehaviour
    {
        public Image m_backGround;
        public Image m_leftImage;
        public Image m_middleImage;
        public Image m_rightImage;
        public Text m_scriptText;
        public Text m_characterNameText;

        public float m_scriptAnimationDelay;

        public void Awake()
        {
            m_leftImage.sprite = null;
            m_rightImage.sprite = null;
            m_middleImage.sprite = null;

            m_leftImage.gameObject.SetActive(false);
            m_middleImage.gameObject.SetActive(false);
            m_rightImage.gameObject.SetActive(false);

            m_scriptText.text = "";
        }

        public void SetData(Sprite backGround, Sprite left, Sprite middle, Sprite right, string script, string characterName, System.Action function)
        {
            if(backGround)
            {
                m_backGround.sprite = backGround;
            }
            else
            {
                m_backGround.sprite = null;
            }

            if(left)
            {
                m_leftImage.gameObject.SetActive(true);
                m_leftImage.sprite = left;
            }
            else
            {
                m_leftImage.gameObject.SetActive(false);
                m_leftImage.sprite = null;
            }

            if (middle)
            {
                m_middleImage.gameObject.SetActive(true);
                m_middleImage.sprite = middle;
            }
            else
            {
                m_middleImage.gameObject.SetActive(false);
                m_middleImage.sprite = null;
            }

            if (right)
            {
                m_rightImage.gameObject.SetActive(true);
                m_rightImage.sprite = right;
            }
            else
            {
                m_rightImage.gameObject.SetActive(false);
                m_rightImage.sprite = null;
            }

            //m_scriptText.text = script;
            m_characterNameText.text = characterName;

            StopAllCoroutines();
            StartCoroutine(ScriptAnimation(script, function));
        }

        public void SetScript(string script)
        {
            StopAllCoroutines();
            m_scriptText.text = script.Replace('^','\n');
        }

        IEnumerator ScriptAnimation(string script, System.Action function)
        {
            string temp = "";
            m_scriptText.text = temp;
            int index = 0;

            while(true)
            {
                yield return new WaitForSeconds(m_scriptAnimationDelay);

                if(script[index] == '^' )
                {
                    temp += "\n";
                    
                }
                else
                    temp += script[index];
                
                
                m_scriptText.text = temp;

                index++;
                if (index > script.Length - 1)
                    break;

            }

            function();
        }
    }
}