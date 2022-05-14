using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// This will control the gameplay, win/lose states on main scene
public class MainController : MonoBehaviour
{
    [SerializeField] public GameObject playerChar;
    public PlayerController playerController;

    public GameObject gameOverDisplay;
    public GameObject gameWinDisplay;

    // Start is called before the first frame update
    void Start()
    {
        playerChar = GetComponent<GameObject>();
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
            gameWinDisplay.SetActive(true);
        }
    }

    public void GameOver()
    {
        if (PlayerController.isPlaying)
        {
            PlayerController.isPlaying = false;
            gameOverDisplay.SetActive(true);
        }
    }
}
