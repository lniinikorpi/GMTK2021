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

    [Header("References")]
    public GameObject sprite;
    public Animator animator;
    public Sprite dieSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableSuperHack()
    {
        sprite.layer = LayerMask.NameToLayer("Enemy");
        animator.Play("enableSuperHack");
        StartCoroutine(DisableSuperHack());
    }

    private IEnumerator DisableSuperHack()
    {
        yield return new WaitForSeconds(superHackTime);
        sprite.layer = LayerMask.NameToLayer("Player");
        animator.Play("disableSuperHack");
    }
    
    public void OnPause()
    {
        GameManager.instance.PauseGame();
    }
}
