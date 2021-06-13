using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public GameObject powerParticle;
    public AudioClip powerClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHit>().TakeDamage(-1);
            GameObject particle = Instantiate(powerParticle, transform.position, Quaternion.identity);
            particle.GetComponent<AudioSource>().clip = powerClip;
            particle.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}
