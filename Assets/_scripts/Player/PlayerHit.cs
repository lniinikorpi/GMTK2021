using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour, IDamageable
{
    private Player _player;
    private int _currentHealth;
    private bool _damageable = true;
    private float _canTakeDamage;

    void Start()
    {
        _player = GetComponent<Player>();
        _currentHealth = _player.maxHealth;
    }

    void Update()
    {
        if(Time.time >= _canTakeDamage)
        {
            _damageable = true;
            GetComponentInChildren<SpriteRenderer>(true).sprite = _player.normalSprite;
        }
    }

    public void Die()
    {
        _player.PlayAudio(_player.deathClip);
        GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(false);
        Instantiate(_player.dieParticle, transform.position, Quaternion.identity);
        GetComponent<BoxCollider2D>().enabled = false;
        _player.isAlive = false;
        StartCoroutine(_player.RestartLevel());
    }

    public void ToMaxHealth()
    {
        _currentHealth = _player.maxHealth;
        UIManager.instance.UpdateHealthBar(_currentHealth);
    }

    public void TakeDamage(int value)
    {
        if (value > 0)
        {
            if (!_damageable)
            return;

            _canTakeDamage = Time.time + _player.dmgBoostTime;
            _damageable = false;
            _player.PlayAudio(_player.hitClip);
            GetComponentInChildren<SpriteRenderer>().sprite = _player.dieSprite;
        }

        _currentHealth -= value;
        if(_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die();
        }
        else if(_currentHealth > _player.maxHealth)
        {
            _currentHealth = _player.maxHealth;
        }
        UIManager.instance.UpdateHealthBar(_currentHealth);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            TakeDamage(other.GetComponent<Enemy>().damage);
        }

        if(other.CompareTag("BombWall"))
        {
            TakeDamage(100);
        }
    }
}
