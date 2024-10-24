using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class CharacterSkinShopItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text statusLable;
        [SerializeField] private Button actionButton;
        [SerializeField] private ImageCharacterDoll doll;

        private CharacterSkinData model;

        void Start()
        {
            actionButton.onClick.AddListener(OnClick);

            CharacterSkinData.OnChange += UpdateInfo;
            PlayerData.OnChangeMoneyValue += UpdateInfo;
        }

        public void SetData(CharacterSkinData _model)
        {
            model = _model;
            doll.SetSkin(_model);

            UpdateInfo();
        }

        private void UpdateInfo()
        {
            if (model.IsPurchased)
            {
                if (model.IsSelected)
                {
                    statusLable.text = "Equiped";
                }
                else
                {
                    statusLable.text = "Not\nEquiped";
                }
            }
            else
            {
                if (PlayerData.Instance.Money >= 20)
                {
                    statusLable.text = "Buy";
                }
                else
                {
                    statusLable.text = "No money";
                }
            }
        }

        private void OnClick()
        {
            GameAudio.Instance.Click();

            if (model.IsPurchased)
            {
                if (!model.IsSelected)
                {
                    model.IsSelected = true;
                }
            }
            else
            {
                if (PlayerData.Instance.Money >= 20)
                {
                    model.IsPurchased = true;

                    PlayerData.Instance.Money -= 20;
                }
            }
        }
    }
}
