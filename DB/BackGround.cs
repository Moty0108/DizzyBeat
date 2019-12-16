using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH
{
    public class BackGround : MonoBehaviour
    {
        public List<Vector2> m_backGroundAreaPoints = new List<Vector2>();

        public SpriteRenderer m_spriteRendrer;

        public Canvas m_canvas;

        // Start is called before the first frame update

        private void Awake()
        {
            //m_spriteRendrer.size = new Vector2(m_spriteRendrer.size.x * m_canvas.GetComponent<RectTransform>().localScale.x, m_spriteRendrer.size.y);
            //transform.localScale = m_canvas.GetComponent<RectTransform>().localScale;
            //Debug.Log(m_canvas.GetComponent<RectTransform>().localScale.x);
        }

        public float GetHeight(float _charaterPosX)
        {
            for (int i = 0; i < m_backGroundAreaPoints.Count - 1; i++)
            {
                if (transform.position.x + m_backGroundAreaPoints[i].x < _charaterPosX && _charaterPosX < transform.position.x + m_backGroundAreaPoints[i + 1].x)
                {

                    return transform.position.y + m_backGroundAreaPoints[i].y;
                }
            }

            return 0;
        }

        public float GetLeft()
        {
            return m_backGroundAreaPoints[0].x + transform.position.x;
        }

        public float GetRight()
        {
            return m_backGroundAreaPoints[m_backGroundAreaPoints.Count - 1].x + transform.position.x;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;

            if (m_backGroundAreaPoints != null)
            {
                for (int i = 0; i < m_backGroundAreaPoints.Count - 1; i++)
                {
                    Gizmos.DrawLine(transform.position + (Vector3)m_backGroundAreaPoints[i], transform.position + (Vector3)m_backGroundAreaPoints[i + 1]);
                }

                for (int i = 0; i < m_backGroundAreaPoints.Count; i++)
                {
                    Gizmos.DrawLine(transform.position + (Vector3)m_backGroundAreaPoints[i], transform.position + (Vector3)m_backGroundAreaPoints[i] + Vector3.down * (m_spriteRendrer.size.y - (m_spriteRendrer.size.y / 2) + m_backGroundAreaPoints[i].y));
                }
            }

        }


        public static void CraeteBackGroundAreaSC(List<Vector2> vectors)
        {
            BackGroundAreaSC temp = (BackGroundAreaSC)ScriptableObject.CreateInstance(typeof(BackGroundAreaSC));
            temp.CreateVector(vectors);
        }

        //[ContextMenu("Test")]
        //public void CraeteBackGroundArea()
        //{
        //    BackGroundAreaSC temp = (BackGroundAreaSC)ScriptableObject.CreateInstance(typeof(BackGroundAreaSC));
        //    temp.CreateVector(m_backGroundAreaPoints);
        //    UnityEditor.AssetDatabase.CreateAsset((ScriptableObject)temp, "Assets/Resources/BackGroundArea/BackGroundArea.asset");
        //}
    }

}