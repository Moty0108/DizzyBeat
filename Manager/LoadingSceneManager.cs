using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TH
{
    public class LoadingSceneManager : MonoBehaviour
    {
        static string m_sceneName;
        public float m_loadingTime;
        public Text m_loadingLabel;
        public Image m_loadingImage;
        public Sprite[] m_loadingSprites;
        // Start is called before the first frame update
        void Start()
        {
            m_loadingImage.sprite = m_loadingSprites[Random.Range(0, m_loadingSprites.Length)];
            SoundManager.Instance.StopAudio(AudioType.BACKGROUND);
            StartCoroutine(Load());
        }

        public static void SceneLoad(string _name)
        {
            m_sceneName = _name;
            SceneManager.LoadScene("LoadingScene");
        }

        IEnumerator Load()
        {
            yield return null;

            AsyncOperation op = SceneManager.LoadSceneAsync(m_sceneName);
            op.allowSceneActivation = false;

            float time = 0;

            while (!op.isDone)
            {
                time += Time.deltaTime;

                if (op.progress >= 0.9f && time >= m_loadingTime)
                    op.allowSceneActivation = true;

                yield return null;
            }
        }
    }
}