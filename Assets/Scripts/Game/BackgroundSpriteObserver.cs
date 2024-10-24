using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    public class BackgroundSpriteObserver : MonoBehaviour
    {
        [SerializeField] private Image backgroundImg;
        
        void Start()
        {
            BackgroundSkinData.OnChange += UpdateSprite;

            UpdateSprite();
        }

        private void UpdateSprite()
        {
            foreach(var skin in PlayerData.Instance.BackgroundSkins)
            {
                if (skin.IsSelected) backgroundImg.sprite = skin.SkinElement;
            }
        }
    }
}