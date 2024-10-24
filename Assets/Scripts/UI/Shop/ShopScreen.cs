using Data;
using System.Collections;
using System.Collections.Generic;
using UI.Shop;
using UnityEngine;

namespace UI
{
    public class ShopScreen : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [SerializeField] private List<CharacterSkinShopItem> characterSkinShopItems = new List<CharacterSkinShopItem>();
        [SerializeField] private List<BackgroundSkinShopItem> backgroundSkinShopItems = new List<BackgroundSkinShopItem>();

        private void Start()
        {
            for (var i = 0; i < PlayerData.Instance.CharacterSkins.Length; i++)
            {
                if (characterSkinShopItems.Count <= i)
                    characterSkinShopItems.Add(Instantiate(characterSkinShopItems[0], characterSkinShopItems[0].transform.parent));

                characterSkinShopItems[i].SetData(PlayerData.Instance.CharacterSkins[i]);
            }

            for (var i = 0; i < PlayerData.Instance.BackgroundSkins.Length; i++)
            {
                if (backgroundSkinShopItems.Count <= i)
                    backgroundSkinShopItems.Add(Instantiate(backgroundSkinShopItems[0], backgroundSkinShopItems[0].transform.parent));

                backgroundSkinShopItems[i].SetData(PlayerData.Instance.BackgroundSkins[i]);
            }
        }

        public void Show()
        {
            animator.Play("Show");

            GameAudio.Instance.Click();
        }

        public void Hide()
        {
            animator.Play("Hide");

            GameAudio.Instance.Click();
        }
    }
}
