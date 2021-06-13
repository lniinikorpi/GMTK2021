using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Color color;
    public List<GameObject> affectedDoors = new List<GameObject>();
    bool isOpen = false;

    private AudioSource audioSource;

    private void Start()
    {
        GetComponentInChildren<SpriteRenderer>().color = color;
        audioSource = GetComponent<AudioSource>();
        foreach (GameObject obj in affectedDoors)
        {
            foreach (SpriteRenderer renderer in obj.GetComponentsInChildren<SpriteRenderer>())
            {
                renderer.color = color;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!isOpen)
            {
                isOpen = true;
                foreach (GameObject obj in affectedDoors)
                {
                    obj.SetActive(false);
                }
                GetComponentInChildren<SpriteRenderer>().color = Color.green;
                audioSource.Play(); 
            }
        }
    }
}
