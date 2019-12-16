using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TH
{
    [CustomEditor(typeof(Character))]
    [CanEditMultipleObjects]
    public class CharacterEditor : Editor
    {
        Character _target;
        List<string> m_idleAnimationName;
        List<int> m_index;
        private void OnEnable()
        {
            _target = (Character)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(30);
            GUIStyle style = new GUIStyle();
            style.fontStyle = FontStyle.Bold;
            style.fontSize = 20;
            GUILayout.Label("IDLE 애니메이션 확률 설정", style);
            GUILayout.Box("각 확률을 0~1 사이의 값으로 설정\n각 확률의 총합이 1이 되어야함", GUILayout.ExpandWidth(true));
            GUILayout.BeginHorizontal();

            if(GUILayout.Button("Idle 애니메이션 가져오기"))
            {
                m_idleAnimationName = new List<string>();
                m_index = new List<int>();

                for(int i = 0; i< _target.GetComponentInChildren<Spine.Unity.SkeletonAnimation>().skeletonDataAsset.GetSkeletonData(false).Animations.Count; i++)
                {
                    Spine.Animation ani = _target.GetComponentInChildren<Spine.Unity.SkeletonAnimation>().skeletonDataAsset.GetSkeletonData(false).Animations.Items[i];

                    if (ani.Name[0] == 'i')
                    {
                        Debug.Log(ani.Name);
                        m_idleAnimationName.Add(ani.Name);
                        m_index.Add(i);
                    }
                }

                //foreach (Spine.Animation ani in _target.GetComponentInChildren<Spine.Unity.SkeletonAnimation>().skeletonDataAsset.GetSkeletonData(false).Animations)
                //{
                //    if (ani.Name[0] == 'i')
                //    {
                //        Debug.Log(ani.Name);
                //        m_idleAnimationName.Add(ani.Name);
                //    }
                //}
                _target.m_oddsOfIdles = new float[m_idleAnimationName.Count];
            }

            if (GUILayout.Button("Idle 애니메이션 확률 초기화"))
            {
                _target.m_oddsOfIdles = new float[0];
            }

            GUILayout.EndHorizontal();

            if (_target.m_oddsOfIdles.Length > 0 && m_index != null)
            {
                for(int i = 0; i < _target.m_oddsOfIdles.Length;i++)
                {
                    string animationName = _target.GetComponentInChildren<Spine.Unity.SkeletonAnimation>().skeletonDataAsset.GetSkeletonData(false).Animations.Items[m_index[i]].ToString();
                    _target.m_oddsOfIdles[i] = EditorGUILayout.Slider(animationName, _target.m_oddsOfIdles[i], 0, 1);
                }
            }

            if(GUI.changed)
            {
                EditorUtility.SetDirty(_target);
            }
        }
    }

}