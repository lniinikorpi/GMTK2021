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

    public void Die()
    {

    }

    public void TakeDamage(int value)
    {
        if (!_damageable)
            return;

        _canTakeDamage = Time.time + _player.dmgBoostTime;
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
    }
}
