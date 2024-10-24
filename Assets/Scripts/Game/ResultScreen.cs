using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    public class ResultScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;

        [SerializeField] private GameObject profitContainer;
        [SerializeField] private TMP_Text coinsProfit;

        [SerializeField] private GameObject totalContainer;
        [SerializeField] private TMP_Text totalScore;

        [SerializeField] private Button okBtn;
        [SerializeField] private Button retryBtn;
        [SerializeField] private Button menuBtn;

        void Start()
        {
            okBtn.onClick.AddListener(MainController.Instance.BackToMenu);
            retryBtn.onClick.AddListener(MainController.Instance.StartGame);
            menuBtn.onClick.AddListener(MainController.Instance.BackToMenu);
        }

        public void Show(bool _isWin)
        {
            gameObject.SetActive(true);

            if(_isWin) GameAudio.Instance.Win();
            else GameAudio.Instance.Lose();

            title.text = _isWin ? "LEVEL COMPLETE!" : "LEVEL FAILED!";

            coinsProfit.text = "+10";
            totalScore.text = PlayerData.Instance.Score.ToString();

            profitContainer.SetActive(_isWin);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
