using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Spine.Unity;

namespace TH
{
    public enum SpineAnimationState
    {
        IDLE, WALK, HANG
    }

    public class Character : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {

        public SkeletonAnimation m_skeletonAnimation;
        public List<string> m_animationName;
        public BackGround m_backGroundInfo;

        public GameObject m_characterPoint;
        public Transform m_characterPivot;
        //Transform m_shadow;
        public Transform m_shadow;
        public float m_hangingHeight;
        public float m_hangingSpeed;

     

        [Range(0, 1)]
        public float m_shadowHideHeight;

        [Range(0, 1)]
        public float m_oddsOfWalk;
        public float m_walkSpeed;

        public List<string> m_walkAnimationName;
        public List<string> m_idleAnimationName;
        public List<string> m_hangAnimationName;



        [HideInInspector]
        public float[] m_oddsOfIdles;

        float m_coroutineTime = 0;
        float m_shadowSize;
        SpriteRenderer m_shadowRenderer;

        

        public SpineAnimationState m_characterState = SpineAnimationState.IDLE;

        Vector3 m_destinationPos;
        Vector3 m_direction = Vector3.zero;
        MeshRenderer m_meshRenderer;

        void Start()
        {
            SetSpine();
            m_meshRenderer = GetComponentInChildren<MeshRenderer>();
            m_shadowRenderer = m_shadow.GetComponent<SpriteRenderer>();
            m_shadowSize = m_shadowRenderer.size.x;
        }

        // Update is called once per frame
        void Update()
        {
            float distance = m_shadow.localPosition.y - m_characterPivot.localPosition.y;
       
            if (distance != 0)
            {
                float newShadowSize;

                newShadowSize = m_shadowSize * Mathf.Clamp((1 - Mathf.Abs(distance) / (m_shadowHideHeight * m_hangingHeight)), 0, 1);

                m_shadow.GetComponent<SpriteRenderer>().size = new Vector2(newShadowSize, newShadowSize);
            }

            m_meshRenderer.sortingOrder = -(int)transform.position.y;
            transform.position = new Vector3(transform.position.x, transform.position.y, m_backGroundInfo.transform.position.z + transform.position.y);
        }

        public void AnimationEndDelegate(Spine.TrackEntry trackEntry)
        {
            if (m_characterState == SpineAnimationState.HANG)
                return;

            if(!trackEntry.Loop)
            {
                if(m_oddsOfWalk > Random.Range(0f, 1f))
                {
                    ChangeCharacterState(SpineAnimationState.WALK, 0, m_walkAnimationName[0], true);
                    return;
                }

                int index = 0;
                if (m_oddsOfIdles.Length > 1)
                {
                    float odds = Random.Range(0f, 1f);
                    float currentOdds = 0;
                    for (int i = 0; i < m_oddsOfIdles.Length; i++)
                    {
                        if (currentOdds < odds && odds <= currentOdds + m_oddsOfIdles[i])
                        {
                            index = i;
                            break;
                        }
                        currentOdds += m_oddsOfIdles[i];
                    }
                }
                
                    ChangeAnimation(0, m_idleAnimationName[index], false);
               
            }
        }
        
        public void ChangeSkeletonData(SkeletonSC skeletonSC)
        {
            SkeletonAnimation temp = GetComponentInChildren<SkeletonAnimation>();
            temp.skeletonDataAsset = (SkeletonDataAsset)skeletonSC.m_skeletonData;
            temp.Initialize(true);
            SetSpine();
        }

        public void SetSpine()
        {
            m_animationName = new List<string>();
            m_walkAnimationName = new List<string>();
            m_hangAnimationName = new List<string>();
            m_idleAnimationName = new List<string>();

            m_direction = (m_destinationPos - transform.position).normalized;
            if (m_direction.x >= 0)
                m_skeletonAnimation.skeleton.ScaleX = -1;
            else
                m_skeletonAnimation.skeleton.ScaleX = 1;

            foreach (Spine.Animation ani in m_skeletonAnimation.skeleton.Data.Animations.ToArray())
            {
                if (ani.Name[0] == 'i')
                    m_idleAnimationName.Add(ani.Name);
                else if (ani.Name[0] == 'w')
                    m_walkAnimationName.Add(ani.Name);
                else if (ani.Name[0] == 'h')
                    m_hangAnimationName.Add(ani.Name);

            }

            StopAllCoroutines();
            m_characterState = SpineAnimationState.IDLE;
            m_skeletonAnimation.state.SetAnimation(0, m_idleAnimationName[0], false);
            m_skeletonAnimation.state.Complete += AnimationEndDelegate;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_characterPoint.SetActive(true);

            StopAllCoroutines();
            StartCoroutine(PlayHangingAnimation());

            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            
            m_characterPoint.SetActive(false);
            StopAllCoroutines();
            StartCoroutine(StopHangingAnimation());
        }


        public void OnDrag(PointerEventData eventData)
        {
            float maxHeight = m_backGroundInfo.GetHeight(transform.position.x);


            if (maxHeight < transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
                return;
            }

            float minHeight = m_backGroundInfo.transform.position.y - m_backGroundInfo.GetComponent<SpriteRenderer>().size.y / 2;

            if (transform.position.y < minHeight)
            {
                transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
                return;
            }

            float left = m_backGroundInfo.GetLeft();

            if (transform.position.x < left)
            {
                transform.position = new Vector3(left, transform.position.y, transform.position.z);
                return;
            }

            float right = m_backGroundInfo.GetRight();

            if (transform.position.x > right)
            {
                transform.position = new Vector3(right, transform.position.y, transform.position.z);
                return;
            }

            transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
        }


        public void ChangeAnimation(int trackIndex, string animationName, bool isLoop)
        {
          
                Debug.Log(transform.root.name + " 애니메이션 변경 : " + animationName);
                //m_skeletonAnimation.state.AddAnimation(trackIndex, animationName, isLoop, 0).MixDuration = 0.3f;
              
                m_skeletonAnimation.state.SetAnimation(trackIndex, animationName, isLoop).MixDuration = 0.3f;
            
            
        }

        public void ChangeCharacterState(SpineAnimationState state, int trackIndex, string animationName, bool isLoop)
        {

            Debug.Log(transform.root.name + " 상태 변경 : " + state.ToString());

            switch (state)
            {
                case SpineAnimationState.WALK:
                    ChangeAnimation(trackIndex, animationName, isLoop);
                 StartCoroutine(MoveCharacter());
                    break;

                case SpineAnimationState.IDLE:
                    ChangeAnimation(trackIndex, animationName, isLoop);
                    break;

                case SpineAnimationState.HANG:
                    ChangeAnimation(trackIndex, animationName, isLoop);
                    break;
            }

        }

        IEnumerator MoveCharacter()
        {
            float x = Random.Range(m_backGroundInfo.GetLeft(), m_backGroundInfo.GetRight());
            float y = Random.Range(m_backGroundInfo.GetHeight(x), m_backGroundInfo.transform.position.y - m_backGroundInfo.GetComponent<SpriteRenderer>().size.y / 2);
            m_destinationPos = new Vector3(x, y, transform.position.z);
            m_direction = (m_destinationPos - transform.position).normalized;
            if (m_direction.x > 0)
                m_skeletonAnimation.skeleton.ScaleX = -1;
            else
                m_skeletonAnimation.skeleton.ScaleX = 1;



            while (true)
            {
                transform.Translate(m_direction * Time.deltaTime * m_walkSpeed);
                //if(Mathf.Abs(transform.position.x - m_destinationPos.x) < 10f && Mathf.Abs(transform.position.y - m_destinationPos.y) < 10f)
                if(Vector2.Distance(transform.position, m_destinationPos) < 10f)
                {
                    ChangeCharacterState(SpineAnimationState.IDLE, 0, m_idleAnimationName[Random.Range(0, m_idleAnimationName.Count - 1)], false);
                    break;
                }

                yield return null;
            }
        }

        IEnumerator PlayHangingAnimation()
        {
            m_characterState = SpineAnimationState.HANG;
            m_skeletonAnimation.state.SetAnimation(0, m_hangAnimationName[0], true);
            //ChangeAnimation(0, m_hangAnimationName[0], true);
            while (true)
            {

                m_coroutineTime += Time.deltaTime * m_hangingSpeed;

                if (m_coroutineTime >= 1)
                {
                    m_coroutineTime = 1;
                    m_characterPivot.localPosition = new Vector3(0, Mathf.Lerp(0, m_hangingHeight, m_coroutineTime), 0);
                    break;
                }

                m_characterPivot.localPosition = new Vector3(0, Mathf.Lerp(0, m_hangingHeight, m_coroutineTime), 0);

                yield return null;
            }
        }

        IEnumerator StopHangingAnimation()
        {
            m_characterState = SpineAnimationState.IDLE;
            //ChangeAnimation(0, m_idleAnimationName[Random.Range(0, m_idleAnimationName.Count - 1)], false);
            m_skeletonAnimation.state.SetAnimation(0, m_idleAnimationName[Random.Range(0, m_idleAnimationName.Count - 1)], false);

            while (true)
            {
                m_coroutineTime -= Time.deltaTime * m_hangingSpeed;

                if (m_coroutineTime <= 0)
                {
                    m_coroutineTime = 0;
                    m_characterPivot.localPosition = new Vector3(0, Mathf.Lerp(0, m_hangingHeight, m_coroutineTime), 0);
                    break;
                }

                m_characterPivot.localPosition = new Vector3(0, Mathf.Lerp(0, m_hangingHeight, m_coroutineTime), 0);

                yield return null;
            }

            
        }

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * m_hangingHeight);

            Gizmos.DrawLine(transform.position + Vector3.left * 50f, transform.position + Vector3.right * 50f);

            Gizmos.DrawLine(transform.position + Vector3.up * m_hangingHeight + Vector3.left * 50f, transform.position + Vector3.up * m_hangingHeight + Vector3.right * 50f);

            UnityEditor.Handles.Label(transform.position + Vector3.up * m_hangingHeight + Vector3.right * 50f, "Hanging Height");

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position + Vector3.up * (m_hangingHeight * m_shadowHideHeight) + Vector3.left * 50f, transform.position + Vector3.up * (m_hangingHeight * m_shadowHideHeight) + Vector3.right * 50f);
            UnityEditor.Handles.Label(transform.position + Vector3.up * (m_hangingHeight * m_shadowHideHeight) + Vector3.right * 50f, "Shadow Hide Height\n(그림자가 완전히 사라지는 높이)");
        }
#endif
    }
}