using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public class BackgroundSkinShopItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text statusLable;
        [SerializeField] private Button actionButton;
        [SerializeField] private Image icon;

        private BackgroundSkinData model;

        void Start()
        {
            actionButton.onClick.AddListener(OnClick);

            BackgroundSkinData.OnChange += UpdateInfo;
            PlayerData.OnChangeMoneyValue += UpdateInfo;
        }

        public void SetData(BackgroundSkinData _model)
        {
            model = _model;
            icon.sprite = _model.SkinElement;

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
