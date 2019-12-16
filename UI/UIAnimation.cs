using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH
{
    public abstract class UIAnimation : ScriptableObject
    {

        public void StartPressAnimation(MonoBehaviour behavior, Transform transform)
        {
            //behavior.StopAllCoroutines();
            //behavior.StartCoroutine(PressAnimation(transform));

            if(CoroutineManger.Instance)
                CoroutineManger.Instance.StartCoroutine(behavior, PressAnimation(transform));
        }

        public void StartExitAnimation(MonoBehaviour behavior, Transform transform)
        {
            //behavior.StopAllCoroutines();
            //behavior.StartCoroutine(ExitAnimation(transform));

            if (CoroutineManger.Instance)
                CoroutineManger.Instance.StartCoroutine(behavior, ExitAnimation(transform));
        }

        protected abstract IEnumerator PressAnimation(Transform transform);

        protected abstract IEnumerator ExitAnimation(Transform transform);
    }
}
