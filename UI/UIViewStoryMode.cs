using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharacterInfo
{
    public Sprite m_character;
    public string m_name;
    public string m_script;
}

public class UIViewStoryMode : MonoBehaviour
{
    public CharacterInfo[] m_characters;

    public Image m_characterImage;
    public Text m_name;
    public Text m_script;
   
    public void OnEnable()
    {
        int random = (int)Random.Range(0, m_characters.Length);
        m_characterImage.sprite = m_characters[random].m_character;
        m_name.text = m_characters[random].m_name;
        m_script.text = m_characters[random].m_script.Replace("\\n", "\n");
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
