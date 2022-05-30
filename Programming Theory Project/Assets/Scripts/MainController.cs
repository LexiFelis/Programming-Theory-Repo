using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// This will control the gameplay, win/lose states on main scene
public class MainController : MonoBehaviour
{
    private GameObject playerChar;
    private PlayerController playerController;
    [SerializeField] private GameObject uiController;
    private MainUIController mainUIController;


    // Start is called before the first frame update
    void Start()
    {
        playerChar = GameObject.Find("Player");
        playerController = playerChar.gameObject.GetComponent<PlayerController>();
        mainUIController = uiController.gameObject.GetComponent<MainUIController>();
    }

    // Update is called once per frame
    void Update()
    {
        UIControls();
    }

    private void UIControls()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMenu();
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ResetGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void GameWin()
    {
        if (PlayerController.isPlaying)
        {
            PlayerController.isPlaying = false;
            mainUIController.GameWinDisplay();
        }
    }

    public void GameOver()
    {
        if (PlayerController.isPlaying)
        {
            PlayerController.isPlaying = false;
            mainUIController.GameOverDisplay();
        }
    }
}
