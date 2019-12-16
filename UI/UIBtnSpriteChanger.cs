using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

namespace TH
{
    public class CharacterSkeleton
    {
        public Character m_character;
        public SkeletonSC m_skeletonSC;

        public CharacterSkeleton(Character character, SkeletonSC skeletonSC)
        {
            m_character = character;
            m_skeletonSC = skeletonSC;
        }

        public void SetSkeletonData()
        {
            m_character.ChangeSkeletonData(m_skeletonSC);
        }
    }

    public class UIBtnSpriteChanger : UIButton
    {
        SpriteRenderer m_image;
        CharacterSkeleton m_vocal, m_base, m_guitar, m_drum, m_keyboard;
        public int m_skin;
        public BackGroundAreaSC m_backGroundSC;
        public BackGround m_backGround;
        public void Set(SpriteRenderer sprite, CharacterSkeleton vocal, CharacterSkeleton bases, CharacterSkeleton guitar, CharacterSkeleton drum, CharacterSkeleton keyboard, BackGround backGround, BackGroundAreaSC backgroundSC)
        {
            m_image = sprite;
            m_vocal = vocal;
            m_guitar = guitar;
            m_base = bases;
            m_drum = drum;
            m_keyboard = keyboard;
            m_backGround = backGround;
            m_backGroundSC = backgroundSC;
        }

        public override void Click()
        {
            GameInfoManager.Instance.m_gameInfo.m_skin = m_skin;
            m_image.sprite = GetComponent<UnityEngine.UI.Image>().sprite;
            m_vocal.SetSkeletonData();
            m_base.SetSkeletonData();
            m_guitar.SetSkeletonData();
            m_drum.SetSkeletonData();
            m_keyboard.SetSkeletonData();
            m_backGround.m_backGroundAreaPoints = m_backGroundSC.GetVector();
        }
        
    }
}