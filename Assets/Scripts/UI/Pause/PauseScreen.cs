using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PauseScreen : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [SerializeField] private TMP_Text titleLable;
        [SerializeField] private Toggle soundToggle;
        [SerializeField] private Toggle musicToggle;

        bool isShow = false;

        public void Show(string _title = "Pause")
        {
            if (isShow) return;

            isShow = true;
            animator.Play("Show");

            titleLable.text = _title;

            soundToggle.onValueChanged.AddListener((b) => { 
                GameAudio.Instance.SoundIsPlaying = b;
                GameAudio.Instance.Click();
            });
            musicToggle.onValueChanged.AddListener((b) => { 
                GameAudio.Instance.MusicIsPlaying = b;
                GameAudio.Instance.Click();
            });

            GameAudio.Instance.Click();
        }

        public void Hide()
        {
            if (!isShow) return;

            isShow = false;
            soundToggle.onValueChanged.RemoveAllListeners();
            musicToggle.onValueChanged.RemoveAllListeners();

            animator.Play("Hide");
        }
    }
}
