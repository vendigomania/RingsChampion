using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MenuScreen : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [SerializeField] private Button shop;
        [SerializeField] private Button options;

        [SerializeField] private PauseScreen pauseScreen;
        [SerializeField] private ShopScreen shopScreen;

        private void Start()
        {
            shop.onClick.AddListener(shopScreen.Show);
            options.onClick.AddListener(() => pauseScreen.Show("Options"));
        }

        public void Show()
        {
            animator.Play("Show");
        }

        public void Hide()
        {
            animator.Play("Hide");
        }
    }
}
