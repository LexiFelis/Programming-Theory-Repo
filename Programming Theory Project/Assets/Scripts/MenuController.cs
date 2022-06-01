using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Script for main menu buttons and features
public class MenuController : MonoBehaviour
{
    [SerializeField] TMP_InputField _nameInput;
    [SerializeField] TextMeshProUGUI currentPlayer;
    [SerializeField] GameObject nameError;

    private void Start()
    {
        _nameInput = GameObject.Find("NameInput (TMP)").GetComponent<TMP_InputField>(); // This is the input field
        CurrentPlayer(); // ABSTRACTION
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void InputName()
    {
        string name = _nameInput.text; // turns text from input field into a string.
        if (name == "") // Only accepts non-null entries, will not save a blank name. Shows error message.
        {
            nameError.gameObject.SetActive(true);
        }
        else
        {
            DataController.instance.SetName(name); // This part sets and saves the name in DataController
            CurrentPlayer();
            nameError.gameObject.SetActive(false);
        }
    }

    private void CurrentPlayer()
    {
        currentPlayer.text = "Current Player: " + DataController.instance.playerName;
    }
}
