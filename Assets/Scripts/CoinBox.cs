using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBox : MonoBehaviour, ITakeShellHits
{
    [SerializeField] private Sprite _disabledSprite;
    [SerializeField] private int _totalCoins = 1;

    private int _remainingCoins;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    public void HandleShellHit(ShellFlipped shellFlipped)
    {
        ReleaseCoin();
    }


    // Start is called before the first frame update
    private void Awake()
    {
        _remainingCoins = _totalCoins;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!(collision.WasHitByPlayer() && collision.WasHitFromBelow()))
            return;

        ReleaseCoin();
    }

    private void ReleaseCoin()
    {
        if (_remainingCoins <= 0) return;

        GameManager.Instance.AddCoin();
        _remainingCoins--;

        _animator.SetTrigger("Hit");

        if (_remainingCoins <= 0)
            _spriteRenderer.sprite = _disabledSprite;
    }
}
