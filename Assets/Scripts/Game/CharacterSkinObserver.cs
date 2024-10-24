using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    public class CharacterSkinObserver : MonoBehaviour
    {
        [SerializeField] private SpriteRendererCharacterDoll doll;

        void Start()
        {
            CharacterSkinData.OnChange += UpdateSprite;

            UpdateSprite();
        }

        private void UpdateSprite()
        {
            foreach (var skin in PlayerData.Instance.CharacterSkins)
            {
                if (skin.IsSelected) doll.SetSkin(skin);
            }
        }
    }
}