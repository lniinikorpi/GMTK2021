using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int currentLevelIndex = 0;
    public bool isPaused = false;
    public List<GameObject> levels = new List<GameObject>();
    public GameObject currentLevelObject;
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
    
    private void Start()
    {
        if (AudioManager.instance.isMuted)
        {
            UIManager.instance.Mute();
            AudioManager.instance.Mute();
        }
        isPaused = false;
        Time.timeScale = 1;
        LoadLevel(currentLevelIndex);
    }

    public void LoadLevel(int value)
    {
        if (currentLevelObject)
        {
            Destroy(currentLevelObject); 
        }
        GameObject newLevel = Instantiate(levels[value], Vector2.zero, Quaternion.identity);
        currentLevelObject = newLevel;
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
