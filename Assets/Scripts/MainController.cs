using Game;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] private MenuScreen menuScreen;
    [SerializeField] private GameManager gameManager;


    public static MainController Instance { get; private set; }

    // Update is called once per frame
    void Start()
    {
        Instance = this;
    }

    public void StartGame()
    {
        menuScreen.Hide();
        gameManager.StartGame();

        GameAudio.Instance.Click();
    }

    public void BackToMenu()
    {
        menuScreen.Show();
        gameManager.ResetGame();

        GameAudio.Instance.Click();
    }
}
