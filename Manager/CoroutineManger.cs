using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH
{

    public class CoroutineManger : Singleton<CoroutineManger>
    {
        Dictionary<MonoBehaviour, Coroutine> m_coroutines = new Dictionary<MonoBehaviour, Coroutine>();

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void StartCoroutine(MonoBehaviour behaviour, IEnumerator coroutine)
        {
            if(m_coroutines.ContainsKey(behaviour))
            {
                if(m_coroutines[behaviour] != null)
                    StopCoroutine(m_coroutines[behaviour]);
                m_coroutines.Remove(behaviour);
            }

            m_coroutines.Add(behaviour, StartCoroutine(coroutine));

            Debug.Log("코루틴__ 객체 : " + behaviour + ", 이름 : " + coroutine);
        }
    }

}