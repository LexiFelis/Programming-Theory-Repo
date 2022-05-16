using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesDisplay;

    [SerializeField] private GameObject player;
    private PlayerController playerController;

    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        livesDisplay.text = "Lives: " + playerController.playerLivesRemain;
    }

}
