using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class UICoinsImage : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        GameManager.Instance.OnCoinsChanged += HandleCoinsAnimation;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnCoinsChanged -= HandleCoinsAnimation;
    }

    private void HandleCoinsAnimation(int coins)
    {
        _animator.SetTrigger("CoinsChanged");
    }
}
