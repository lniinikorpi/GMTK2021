using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int currentLevel = 1;
    public bool isPaused = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void LoadLevel(int value)
    {

    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        if(isPaused)
        {
            Time.timeScale = 0;
            UIManager.instance.pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            UIManager.instance.pausePanel.SetActive(false);
        }
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
