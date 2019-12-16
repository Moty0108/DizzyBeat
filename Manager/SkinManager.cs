using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH
{
    public class Skin
    {
        public CharacterSkeleton[] m_skins;
        public Sprite m_backGroundSprite;
        public BackGroundAreaSC m_backGroundSC;
        public BackGround m_backGround;

        public Skin(CharacterSkeleton _vocal, CharacterSkeleton _base, CharacterSkeleton _guitar, CharacterSkeleton _drum, CharacterSkeleton _keyboard, Sprite _backGroundSprite, BackGroundAreaSC _areaSC, BackGround _backGround)
        {
            m_skins = new CharacterSkeleton[5];
            m_skins[0] = _vocal;
            m_skins[1] = _base;
            m_skins[2] = _guitar;
            m_skins[3] = _drum;
            m_skins[4] = _keyboard;
            m_backGroundSprite = _backGroundSprite;
            m_backGroundSC = _areaSC;
            m_backGround = _backGround;
        }

        public void Set()
        {
            foreach(CharacterSkeleton cs in m_skins)
            {
                cs.SetSkeletonData();
            }

            m_backGround.m_backGroundAreaPoints = m_backGroundSC.GetVector();
            m_backGround.GetComponent<SpriteRenderer>().sprite = m_backGroundSprite;
        }
    }

    public class SkinManager : Singleton<SkinManager>
    {
        public List<Skin> m_skins = new List<Skin>();

        public void SetSkin(int index)
        {
            m_skins[index].Set();
        }
    }

}