using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    public List<GameObject> hearts = new List<GameObject>();

    #region singelton
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
    #endregion

    void Start()
    {
        InitializeUI();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeUI()
    {
        UpdateHealthBar(3);
    }

    public void UpdateHealthBar(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if(i < currentHealth)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }
}