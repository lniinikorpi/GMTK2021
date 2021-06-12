using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            ToNextLevel();
        }
    }

    void ToNextLevel()
    {
        GameManager.instance.currentLevel++;
        GameManager.instance.LoadLevel(GameManager.instance.currentLevel);
    }
}
