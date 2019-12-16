using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

namespace TH
{
    public class BackGroundTableLoader : MonoBehaviour, DataUpdateObserver, DataInitObserver
    {
        public GameObject m_contentPrefab;
        public GameObject m_BackPrefab;
        public List<Character> m_char = new List<Character>();
        List<Dictionary<string, object>> m_list;

        public SpriteRenderer m_target;

        GameObject[] m_objs;

        GameObject m_objsBack;
        public string BackString;
        public Character m_vocal, m_base, m_guitar, m_drum, m_keyboard;
        public BackGround m_backGround;

        private void Awake()
        {
            InitData();
        }

        private void OnEnable()
        {
            UpdateData();
        }

        public void ChangerSprite()
        {
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateData()
        {
            for (int i = 0; i < m_list.Count; i++)
            {
                m_objs[i].GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(!UserInfoDBManager.Instance.m_user.m_backGroundInfos[m_objs[i].name].m_isOpen);
                //m_objsBack.GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(!UserInfoDBManager.Instance.m_user.m_backGroundInfos[m_objs[i].name].m_isOpen);
            }
        }

        public void InitData()
        {
            m_list = DBManager.Instance.GetBackGroundData();

            m_objs = new GameObject[m_list.Count];
            m_objsBack = new GameObject();

            for (int i = 0; i < m_list.Count; i++)
            {
                m_objs[i] = Instantiate(m_contentPrefab);
                m_objs[i].transform.SetParent(transform);
                m_objs[i].transform.GetComponent<RectTransform>().sizeDelta = new Vector3(512, 288);
                m_objs[i].name = m_list[i]["FileName"].ToString();
                m_objs[i].transform.localScale = new Vector3(1.16f, 1.8f, 1f);
                m_objs[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("BackGroundImage/" + m_list[i]["FileName"].ToString());
                m_objs[i].AddComponent<UIBtnSpriteChanger>().Set(m_target, 
                    new CharacterSkeleton(m_vocal, Resources.Load<SkeletonSC>("SpineSC/" + m_list[i]["VocalSCName"].ToString())), 
                    new CharacterSkeleton(m_base, Resources.Load<SkeletonSC>("SpineSC/" + m_list[i]["BaseSCName"].ToString())),
                    new CharacterSkeleton(m_guitar, Resources.Load<SkeletonSC>("SpineSC/" + m_list[i]["GuitarSCName"].ToString())),
                    new CharacterSkeleton(m_drum, Resources.Load<SkeletonSC>("SpineSC/" + m_list[i]["DrumSCName"].ToString())), 
                    new CharacterSkeleton(m_keyboard, Resources.Load<SkeletonSC>("SpineSC/" + m_list[i]["KeyboardSCName"].ToString())),
                    m_backGround,
                    Resources.Load<BackGroundAreaSC>("BackGroundArea/" + m_list[i]["BackGroundSC"].ToString())
                    );
                m_objs[i].GetComponent<UIBtnSpriteChanger>().m_skin = i;
                m_objs[i].GetComponentsInChildren<Image>()[1].gameObject.SetActive(!UserInfoDBManager.Instance.m_user.m_backGroundInfos[m_list[i]["FileName"].ToString()].m_isOpen);

                SkinManager.Instance.m_skins.Add(
                    new Skin(
                        new CharacterSkeleton(m_vocal, Resources.Load<SkeletonSC>("SpineSC/" + m_list[i]["VocalSCName"].ToString())),
                        new CharacterSkeleton(m_base, Resources.Load<SkeletonSC>("SpineSC/" + m_list[i]["BaseSCName"].ToString())),
                        new CharacterSkeleton(m_guitar, Resources.Load<SkeletonSC>("SpineSC/" + m_list[i]["GuitarSCName"].ToString())),
                        new CharacterSkeleton(m_drum, Resources.Load<SkeletonSC>("SpineSC/" + m_list[i]["DrumSCName"].ToString())),
                        new CharacterSkeleton(m_keyboard, Resources.Load<SkeletonSC>("SpineSC/" + m_list[i]["KeyboardSCName"].ToString())),
                        Resources.Load<Sprite>("BackGroundImage/" + m_list[i]["FileName"].ToString()),
                        Resources.Load<BackGroundAreaSC>("BackGroundArea/" + m_list[i]["BackGroundSC"].ToString()),
                        m_backGround
                        )
                    );

                m_objsBack = Instantiate(m_BackPrefab);
                m_objsBack.transform.SetParent(m_objs[i].transform);
                m_objsBack.transform.GetComponent<RectTransform>().sizeDelta = new Vector3(512, 288);
              
                m_objsBack.transform.localScale = new Vector3(1.05f, 0.575f, 1f);
                m_objsBack.GetComponent<Image>().sprite = Resources.Load<Sprite>("BackGroundImage/" + BackString);
               

            }
            for (int i = 0; i < m_char.Count; i++)
            {
                m_char[i].SetSpine();
            }
            GetComponent<RectTransform>().sizeDelta = new Vector2(512 * (m_list.Count + 1), GetComponent<RectTransform>().sizeDelta.y);
        }
    }
}