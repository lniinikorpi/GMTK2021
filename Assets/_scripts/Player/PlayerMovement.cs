using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Player _player;
    private Vector2 _movement;
    private float _movementSpeed;
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
        _movementSpeed = _player.movementSpeed;
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(!_player.isAlive)
        {
            _movement = Vector2.zero;
        }
        MovePlayer();
    }

    public void OnMove(InputValue value)
    {
        if(!_player.isAlive)
        {
            return;
        }
        _movement = value.Get<Vector2>();
    }
    void MovePlayer()
    {
        Vector3 velocity = new Vector2(_movement.x, _movement.y) * _movementSpeed;
        velocity = transform.TransformDirection(velocity);
        _rb.velocity = velocity;
    }
}
