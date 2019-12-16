using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH
{

    [CreateAssetMenu(menuName = "SC/UI Animation/SizeAnimation")]
    public class UIAnimSize : UIAnimation
    {
        public AnimationCurve m_xSize;
        public AnimationCurve m_ySize;
        float time = 0;

        protected override IEnumerator ExitAnimation(Transform transform)
        {
            RectTransform rect = transform.GetComponent<RectTransform>();

            float maxTime = Mathf.Max(m_xSize.keys[m_xSize.length - 1].time, m_ySize.keys[m_ySize.length - 1].time);

            
            while (true)
            {
                time -= Time.deltaTime;

                rect.localScale = new Vector3(m_xSize.Evaluate(time), m_ySize.Evaluate(time), 1);
                if (time < 0)
                    break;

                yield return null;
            }
        }

        protected override IEnumerator PressAnimation(Transform transform)
        {
            RectTransform rect = transform.GetComponent<RectTransform>();
            time = 0;

            float maxTime = Mathf.Max(m_xSize.keys[m_xSize.length - 1].time, m_ySize.keys[m_ySize.length - 1].time);

            
            while (true)
            {
                time += Time.deltaTime;

                rect.localScale = new Vector3(m_xSize.Evaluate(time), m_ySize.Evaluate(time), 1);

                if (time > maxTime)
                    break;

                yield return null;
            }
        }


    }

}