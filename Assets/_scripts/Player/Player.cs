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
        StartCoroutine(DisableSuperHack());
    }

    private IEnumerator DisableSuperHack()
    {
        yield return new WaitForSeconds(superHackTime);
        sprite.layer = LayerMask.NameToLayer("Player");
        animator.Play("disableSuperHack");
        PlayAudio(powerdownClip);
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
        GetComponent<PlayerHit>().TakeDamage(-maxHealth);
        GetComponent<BoxCollider2D>().enabled = true;
        isAlive = true;
    }
}
