using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesDisplay;

    [SerializeField] private GameObject player;
    private PlayerController playerController;

    [SerializeField] private GameObject gameOverDisplay;
    [SerializeField] private TextMeshProUGUI gameOverMessage;
    [SerializeField] private GameObject gameWinDisplay;
    [SerializeField] private TextMeshProUGUI gameWinMessage;

    private void Start()
    {
        UIInitialise(); // ABSTRACTION
    }

    private void UIInitialise()
    {
        gameOverDisplay.SetActive(false);
        gameWinDisplay.SetActive(false);
        playerController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        // Updates the life display. Not sure if worth abstracting.
        livesDisplay.text = "Lives: " + playerController.playerLivesRemain;
    }

    public void GameOverDisplay()
    {
        string name = DataController.instance.playerName;
        gameOverMessage.text = "Sorry " + name + ", but that's";
        gameOverDisplay.SetActive(true);
    }

    public void GameWinDisplay()
    {
        string name = DataController.instance.playerName;
        gameWinMessage.text = "Well done " + name + "...";
        gameWinDisplay.SetActive(true);
    }
}
