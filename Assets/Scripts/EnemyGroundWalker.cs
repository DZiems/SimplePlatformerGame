using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundWalker : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private GameObject spawnOnDiePrefab;

    private Collider2D _collider;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;

    private Vector2 _direction = Vector2.left;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.WasHitByPlayer()) 
        {
            var playerMovementController = collision.collider.GetComponent<PlayerMovementController>();
            if (collision.WasHitFromAbove())
                HandleStompedOn(playerMovementController);
            else
                GameManager.Instance.KillPlayer(playerMovementController);
        }
    }

    private void HandleStompedOn(PlayerMovementController playerMovementController)
    {

        playerMovementController.Bounce();

        if (spawnOnDiePrefab != null)
            Instantiate(spawnOnDiePrefab, transform.position, transform.rotation);


        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        _rigidbody2D.position += _direction * _speed * Time.fixedDeltaTime;
    }

    //lateupdate called after fixed update and regular update
    private void LateUpdate()
    {
        if (ReachedEdge() || HitNonPlayer())
            SwitchDirections();
    }

    private bool ReachedEdge()
    {
        float x = GetForwardX();
        float y = _collider.bounds.min.y;

        Vector2 origin = new Vector2(x, y);

        var originRaycastHit = Physics2D.Raycast(origin, Vector2.down, 0.1f);
        return originRaycastHit.collider == null;
    }

    private bool HitNonPlayer()
    {
        float x = GetForwardX();
        float y = transform.position.y;

        Vector2 origin = new Vector2(x, y);
        Debug.DrawRay(origin, _direction * 0.1f);

        var originRaycastHit = Physics2D.Raycast(origin, _direction, 0.1f);

        return originRaycastHit.collider != null && 
            !originRaycastHit.collider.isTrigger &&
            originRaycastHit.collider.GetComponent<PlayerMovementController>() == null;
    }

    private float GetForwardX()
    {
        return _direction.x == 1 ? _collider.bounds.max.x + 0.1f : _collider.bounds.min.x - 0.1f;
    }

    private void SwitchDirections()
    {
        _direction *= -1;
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
}
