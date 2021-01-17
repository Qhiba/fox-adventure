using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int totalGem;

    public GameObject WinPanel;
    public GameObject LosePanel;
    public GameObject GameplayPanel;
    public Text gemText;
    public Text winGemText;

    private PlayerController playerController;

    public void Start()
    {
        Time.timeScale = 1;
        Data.gem = 0;
        Data.isDead = false;
        Data.isWon = false;
    }

    public void Update()
    {
        gemText.text = "Gem Collected: " + Data.gem + " / " + totalGem;

        if (Data.isDead)
        {
            GameLoss();
        }

        if (!Data.isDead && Data.isWon)
        {
            GameWon();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Debug.Log("Game Resumed");
    }

    public void GameLoss()
    {
        PauseGame();
        GameplayPanel.SetActive(false);
        LosePanel.SetActive(true);
    }

    public void GameWon()
    {
        PauseGame();
        GameplayPanel.SetActive(false);
        WinPanel.SetActive(true);
        winGemText.text = "Your Gem: " + Data.gem;
    }
}
