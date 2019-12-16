using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TH
{
    public class ScrollSnap : MonoBehaviour
    {
        public AnimationCurve m_animation;

        [Tooltip("스크롤할 객체가 들어 있는 트랜스폼")]
        public RectTransform panel;

        [Tooltip("스크롤할 객체")]
        public GameObject[] m_obj;

        [Tooltip("스크롤할 객체의 중심이 될 트랜스폼")]
        public RectTransform center;

        [Tooltip("투명 효과 힘\n기본값 = 0.5")]
        public float m_fadePower = 0.5f;

        [Tooltip("스크롤할 객체가 왼족으로 밀리는 힘\n기본값 = -200")]
        public float m_movingPower = -200f;

        [Tooltip("스크롤할 객체 전체가 왼쪽으로 이동하는 값\n기본값 = -100")]
        public float m_movingOffset = -100f;

        [Tooltip("스크롤할 객체가 고정이 되기 위한 최소 값\n기본값 = 5")]
        public float m_fixMinRange = 5f;

        [Tooltip("스크롤할 객체를 고정하는 힘\n기본값 = 1")]
        public float m_fixPower = 1f;

        [Tooltip("스크롤할 객체가 고정되기 위한 스크롤 최소 속도\n스크롤 속도가 FixMinSpeed 아래로 떨어지면 고정됨\n기본값 = 1000")]
        public float m_fixMinSpeed = 100f;

        [Tooltip("???")]
        public int startButton = 1;

        [Tooltip("현재 선택된 오브젝트 넘버")]
        public int minObjNum;

        [Tooltip("스크롤 될 때 재생될 사운드")]
        public AudioClip m_audioClip;

        [HideInInspector]
        public SelectedMusicInfo m_info;

        [HideInInspector]
        public GameObject m_musicInfoPrefab;

        float[] distance;
        float[] distReposition;
        bool dragging = false;
        int objDistance;
        int objLength;
        bool messageSend = false;

        List<Dictionary<string, object>> m_list;

        int m_musicDataHeadNum = 0;
        int m_musicDataTailNum = 0;
        RectTransform[] m_objRectTransform;
        ScrollRect m_scollRect;

        Vector2 temp;

        // Start is called before the first frame update
        void Awake()
        {
            
            temp = center.anchoredPosition;
            //temp = center.transform.position;
            m_list = DBManager.Instance.GetMusicData();

            m_musicDataHeadNum = 0;
            m_musicDataTailNum = 0;

            m_objRectTransform = new RectTransform[m_obj.Length];
            for (int i = 0; i < m_obj.Length; i++)
            {
                m_objRectTransform[i] = m_obj[i].GetComponent<RectTransform>();
            }

            m_scollRect = GetComponent<ScrollRect>();

            for (int i = 0; i < m_obj.Length; i++)
            {

                MusicInfo info = m_obj[i].GetComponent<MusicInfo>();


                info.SetInfo(m_list[m_musicDataTailNum]["MusicName"].ToString(), m_list[m_musicDataTailNum]["Artist"].ToString(), i, i, m_list[m_musicDataTailNum][GameInfoManager.Instance.m_gameInfo.m_mode.ToString() + "Level"].ToString());


                m_obj[i].GetComponentInChildren<Text>().text = info.m_name;
                m_musicDataTailNum++;

                if (m_musicDataTailNum > m_list.Count - 1)
                {
                    m_musicDataTailNum = 0;
                }
            }



            objLength = m_obj.Length;
            distance = new float[objLength];
            distReposition = new float[objLength];

            objDistance = (int)Mathf.Abs(m_objRectTransform[1].anchoredPosition.y - m_objRectTransform[0].anchoredPosition.y);

            panel.anchoredPosition = new Vector2(0, (startButton - 1) * -objDistance);



            ValueChanged(1);
        }

        bool flag = true;

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < m_obj.Length; i++)
            {
                distReposition[i] = center.position.y - m_objRectTransform[i].position.y;
                distance[i] = Mathf.Abs(distReposition[i]);

                // DownScroll
                if (distReposition[i] > panel.sizeDelta.y / 2 * transform.root.transform.localScale.x)
                {
                    float curY = m_objRectTransform[i].anchoredPosition.y;
                    float curX = m_objRectTransform[i].anchoredPosition.x;

                    Vector2 newAnchoredPos = new Vector2(curX, curY + (objLength * objDistance));
                    m_objRectTransform[i].anchoredPosition = newAnchoredPos;

                    // 스크롤 방향 전환시 증감소 안함
                    if (flag)
                    {
                        m_musicDataHeadNum--;
                        m_musicDataTailNum--;
                    }

                    if(m_musicDataTailNum < 0)
                    {
                        m_musicDataTailNum = m_list.Count - 1;
                    }
                    if(m_musicDataHeadNum < 0)
                    {
                        m_musicDataHeadNum = m_list.Count - 1;
                    }

                    MusicInfo info = m_obj[i].GetComponent<MusicInfo>();
                    info.SetInfo(m_list[m_musicDataHeadNum]["MusicName"].ToString(), m_list[m_musicDataHeadNum]["Artist"].ToString(), i, i, m_list[m_musicDataHeadNum][GameInfoManager.Instance.m_gameInfo.m_mode.ToString() + "Level"].ToString());
                    m_obj[i].GetComponentInChildren<Text>().text = info.m_name;

                    flag = true;
                }

                // UpScroll
                if (distReposition[i] < -panel.sizeDelta.y / 2 * transform.root.transform.localScale.x)
                {
                    float curY = m_objRectTransform[i].anchoredPosition.y;
                    float curX = m_objRectTransform[i].anchoredPosition.x;

                    Vector2 newAnchoredPos = new Vector2(curX, curY - (objLength * objDistance));
                    m_objRectTransform[i].anchoredPosition = newAnchoredPos;

                    // 스크롤 방향 전환시 증감소 안함
                    if (!flag)
                    {
                        m_musicDataHeadNum++;
                        m_musicDataTailNum++;
                    }
                    

                    if (m_musicDataTailNum > m_list.Count - 1)
                    {
                        m_musicDataTailNum = 0;
                    }
                    if(m_musicDataHeadNum > m_list.Count - 1)
                    {
                        m_musicDataHeadNum = 0;
                    }

                    MusicInfo info = m_obj[i].GetComponent<MusicInfo>();
                    info.SetInfo(m_list[m_musicDataTailNum]["MusicName"].ToString(), m_list[m_musicDataTailNum]["Artist"].ToString(), i, i, m_list[m_musicDataTailNum][GameInfoManager.Instance.m_gameInfo.m_mode.ToString() + "Level"].ToString());
                    m_obj[i].GetComponentInChildren<Text>().text = info.m_name;

                    flag = false;
                }


                


                // 디버그용
                Debug.DrawLine(new Vector2(transform.position.x, transform.position.y + panel.sizeDelta.y / 2 * transform.root.transform.localScale.x), new Vector2(transform.position.x, transform.position.y - panel.GetComponent<RectTransform>().sizeDelta.y / 2 * transform.root.transform.localScale.x), Color.cyan);
                Debug.DrawLine(new Vector2(transform.position.x + panel.sizeDelta.x * transform.root.transform.localScale.x, transform.position.y + panel.sizeDelta.y / 2 * transform.root.transform.localScale.x), new Vector2(transform.position.x + panel.sizeDelta.x * transform.root.transform.localScale.x, transform.position.y - panel.sizeDelta.y / 2 * transform.root.transform.localScale.x), Color.cyan);
            }

            float minDistance = Mathf.Min(distance);

            for (int i = 0; i < m_obj.Length; i++)
            {
                if (minDistance == distance[i])
                {
                    if (minObjNum != i)
                    {
                        ValueChanged(i);
                    }

                    minObjNum = i;

                }
            }

            if (!dragging)
            {
                LerpToObj((int)-m_objRectTransform[minObjNum].anchoredPosition.y);
            }

            // x 러프, 알파 조절
            for (int i = 0; i < m_obj.Length; i++)
            {
                float dist = Mathf.Abs(center.transform.position.y - m_obj[i].transform.position.y) / transform.root.localScale.y;

                //float x = Mathf.Lerp(center.anchoredPosition.x, center.anchoredPosition.x + m_movingPower * transform.root.localScale.x, dist / (objDistance * (m_obj.Length / 2)));
                float x = Mathf.Lerp(temp.x, temp.x + m_movingPower /* * transform.root.localScale.x*/, dist / (objDistance * (m_obj.Length / 2)));

                x += m_movingOffset;// * transform.root.localScale.x;

                m_objRectTransform[i].anchoredPosition = new Vector2(x, m_objRectTransform[i].anchoredPosition.y);

                Color color = m_obj[i].GetComponentInChildren<Image>().color;
                color.a = Mathf.Lerp(1f, 0f, dist / (objDistance * (m_obj.Length / 2)) * m_fadePower);
                m_obj[i].GetComponentInChildren<Image>().color = color;
            }

        }

        

        private void Selected()
        {
            if (!dragging && messageSend)
            {
                center.gameObject.SetActive(true);
                StopAllCoroutines();
                StartCoroutine(StartAnimation(center));
                SoundManager.Instance.StopAudio(AudioType.BACKGROUND);
                AudioClip clip = (AudioClip)Resources.Load("Music/" + m_info.m_musicName);
                if (clip)
                {
                    SoundManager.Instance.SetClip(AudioType.BACKGROUND, clip);
                    //SoundManager.Instance.GetAudipSource(AudioType.BACKGROUND).time = 100f;
                    SoundManager.Instance.PlayAudio(AudioType.BACKGROUND);
                }
                else
                {
                    Debug.Log("파일명 : " + m_info.m_musicName);
                    Debug.LogError("음악 리소스 없음!! Resource/Music폴더안에 음악파일과 테이블 음악이름이 일치하는지 확인할것!");
                }

                messageSend = false;
            }

            //SoundManager.Instance.PlayAudio(m_audioClip);
        }

        void ValueChanged(int index)
        {
            messageSend = true;
            StopAllCoroutines();
            

            for (int i = 0; i < m_obj.Length; i++)
            {
                if (i == index)
                {
                    MusicInfo info = m_obj[i].GetComponent<MusicInfo>();
                    Difficulty difficulty = GameInfoManager.Instance.m_gameInfo.m_difficulty;
                    m_info.SetData(info.m_name, info.m_artist, UserInfoDBManager.Instance.m_user.GetUserMusicInfo(info.m_name, GameInfoManager.Instance.m_gameInfo.m_mode).GetCombo(difficulty), UserInfoDBManager.Instance.m_user.GetUserMusicInfo(info.m_name, GameInfoManager.Instance.m_gameInfo.m_mode).GetIsClear(difficulty), UserInfoDBManager.Instance.m_user.GetUserMusicInfo(info.m_name, GameInfoManager.Instance.m_gameInfo.m_mode).GetScore(difficulty), info.m_levels[(int)difficulty - 1]);
                    GameInfoManager.Instance.SetMusic(info.m_name);
                    GameInfoManager.Instance.m_gameInfo.m_levels = info.m_levels;
                    continue;
                }
                m_obj[i].SetActive(true);
            }

            
            center.gameObject.SetActive(false);

            for (int j = 0; j < DBManager.Instance.GetMusicDataLength(); j++)
            {
                if (DBManager.Instance.GetMusicDataObject(j, "MusicName").ToString() == GameInfoManager.Instance.m_gameInfo.m_songName)
                {

                    switch (GameInfoManager.Instance.m_gameInfo.m_difficulty)
                    {
                        // 귀찮아서 바꺼야함
                        case Difficulty.EASY:
                            switch(GameInfoManager.Instance.m_gameInfo.m_mode)
                            {
                                case MODE.VOCAL:
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(j, "EASY").ToString());
                                    break;

                                case MODE.GUITAR:
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(j, "GUITAREASY").ToString());
                                    break;

                                case MODE.DRUM:
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(j, "DRUMEASY").ToString());
                                    break;

                                case MODE.KEYBOARD:
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(j, "KEYBOARDEASY").ToString());
                                    break;
                            }
                            
                            break;
                        case Difficulty.MEDIUM:
                            switch (GameInfoManager.Instance.m_gameInfo.m_mode)
                            {
                                case MODE.VOCAL:
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(j, "MEDIUM").ToString());
                                    break;

                                case MODE.GUITAR:
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(j, "GUITARMEDIUM").ToString());
                                    break;

                                case MODE.DRUM:
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(j, "DRUMMEDIUM").ToString());
                                    break;

                                case MODE.KEYBOARD:
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(j, "KEYBOARDMEDIUM").ToString());
                                    break;
                            }
                            break;
                        case Difficulty.HARD:
                            switch (GameInfoManager.Instance.m_gameInfo.m_mode)
                            {
                                case MODE.VOCAL:
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(j, "HARD").ToString());
                                    break;

                                case MODE.GUITAR:
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(j, "GUITARHARD").ToString());
                                    break;

                                case MODE.DRUM:
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(j, "DRUMHARD").ToString());
                                    break;

                                case MODE.KEYBOARD:
                                    GameInfoManager.Instance.SetMusicCSV(DBManager.Instance.GetMusicDataObject(j, "KEYBOARDHARD").ToString());
                                    break;
                            }
                            break;
                    }
                    break;
                }
            }

            SoundManager.Instance.PlayAudio(AudioType.EFFECT, m_audioClip);
        }

        void LerpToObj(int position)
        {
            float newY = Mathf.Lerp(panel.anchoredPosition.y, position + center.anchoredPosition.y, Time.deltaTime * m_fixPower);

            if (Mathf.Abs(position + center.anchoredPosition.y - panel.anchoredPosition.y) < (m_fixMinRange * transform.root.transform.localScale.x) && m_scollRect.velocity.magnitude < m_fixMinSpeed)
            {
                newY = position + center.anchoredPosition.y;

                
                Selected();
            
            }
            
            


            if (Mathf.Abs(newY) >= Mathf.Abs(position + center.anchoredPosition.y) - 4f && Mathf.Abs(newY) <= Mathf.Abs(position + center.anchoredPosition.y) + 4 && !messageSend)
            {
                
                //messageSend = true;
            }

            Vector2 newPosition = new Vector2(panel.anchoredPosition.x, newY);

            panel.anchoredPosition = newPosition;
        }

        public void StartDrag()
        {
            //messageSend = false;
            dragging = true;
        }

        public void EndDrag()
        {
            dragging = false;
        }

        IEnumerator StartAnimation(Transform obj)
        {
            float move;
            move = -obj.GetComponent<RectTransform>().sizeDelta.x * 2;
            RectTransform rt = obj.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector3(move, rt.anchoredPosition.y, 0);
            //obj.transform.position = new Vector3(move, obj.transform.position.y, obj.transform.position.z);
            float time = 0;
            while (true)
            {
                time += Time.deltaTime;

                //move += Time.deltaTime * 10000;
                //if (move > 0)
                //    break;
                

                if(time > m_animation.keys[m_animation.length-1].time)
                {
                    break;
                }
                //obj.transform.position = new Vector3(m_animation.Evaluate(time), obj.transform.position.y, obj.transform.position.z);
                rt.anchoredPosition = new Vector3(m_animation.Evaluate(time), rt.anchoredPosition.y, 0);
                //m_obj[minObjNum].transform.position = new Vector3(m_animation.Evaluate(1 - time), m_obj[minObjNum].transform.position.y, m_obj[minObjNum].transform.position.z);
                RectTransform objRt = m_obj[minObjNum].GetComponent<RectTransform>();
                objRt.anchoredPosition = new Vector3(m_animation.Evaluate(1 - time) - m_movingOffset * 2, objRt.anchoredPosition.y, 0);
                yield return null;


            }

            
        }
    }

}