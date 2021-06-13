using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 3;
    public int movementSpeed = 5;
    public float dmgBoostTime = .5f;
    public float superHackTime = 5;
    [HideInInspector]
    public bool isAlive = true;
    [HideInInspector]
    public bool superHackActive = false;

    [Header("References")]
    public GameObject sprite;
    public Animator animator;
    public Sprite normalSprite;
    public Sprite dieSprite;
    public AudioSource audioSource;
    public AudioClip hitClip;
    public AudioClip deathClip;
    public AudioClip powerupClip;
    public AudioClip powerdownClip;
    public GameObject dieParticle;

    public void EnableSuperHack()
    {
        sprite.layer = LayerMask.NameToLayer("Enemy");
        animator.Play("enableSuperHack");
        PlayAudio(powerupClip);
        superHackActive = true;
        StartCoroutine(DisableSuperHack());
    }

    private IEnumerator DisableSuperHack()
    {
        float timeElapsed = 0;
        while(timeElapsed < superHackTime && superHackActive)
        {
            yield return new WaitForSeconds(.1f);
            timeElapsed += .1f;
        }
        if (superHackActive)
        {
            sprite.layer = LayerMask.NameToLayer("Player");
            animator.Play("disableSuperHack");
            PlayAudio(powerdownClip);
            superHackActive = false; 
        }
    }

    private IEnumerator DisableSuperHackLevelFinnish()
    {
        if(superHackActive)
        {
            animator.Play("disableSuperHack");
            PlayAudio(powerdownClip);
            superHackActive = false;
            sprite.layer = LayerMask.NameToLayer("Player");
            yield return new WaitForSeconds(.2f);
        }
        animator.Play("finnishLevel");
        SetIsAlive(0);
    }
    
    public void OnPause()
    {
        GameManager.instance.PauseGame();
    }

    public void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1);
        transform.position = Vector2.zero;
        GetComponentInChildren<SpriteRenderer>(true).gameObject.SetActive(true);
        GetComponent<PlayerHit>().ToMaxHealth();
        GetComponent<BoxCollider2D>().enabled = true;
        isAlive = true;
        GameManager.instance.LoadLevel(GameManager.instance.currentLevelIndex);
        animator.Play("enterLevel");
    }

    public void CompleteLevel()
    {
        StartCoroutine(DisableSuperHackLevelFinnish());
    }

    public void LoadNewLevel()
    {
        GameManager.instance.currentLevelIndex++;
        if(GameManager.instance.currentLevelIndex == GameManager.instance.levels.Count)
        {
            UIManager.instance.WinGame();
            isAlive = false;
        }
        else
        {
            GameManager.instance.LoadLevel(GameManager.instance.currentLevelIndex);
            GetComponent<PlayerHit>().ToMaxHealth();
            transform.position = Vector2.zero;
            animator.Play("enterLevel"); 
        }
    }

    public void SetIsAlive(int value)
    {
        isAlive = value == 1;
    }
}
