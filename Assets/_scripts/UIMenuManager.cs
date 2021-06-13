using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenuManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject helpPanel;
    public GameObject muteButton;
    public GameObject unMuteButton;

    private void Awake()
    {
        mainPanel.SetActive(true);
        helpPanel.SetActive(false);
    }

    private void Start()
    {
        if (AudioManager.instance.isMuted)
        {
            unMuteButton.SetActive(true);
            muteButton.SetActive(false);
            Mute();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Mute()
    {
        AudioManager.instance.isMuted = true;
        AudioManager.instance.Mute();
    }

    public void UnMute()
    {
        AudioManager.instance.isMuted = false;
        AudioManager.instance.UnMute();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
