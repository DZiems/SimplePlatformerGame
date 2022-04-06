using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class UILivesImage : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        GameManager.Instance.OnLivesChanged += HandleHeartAnimation;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnLivesChanged -= HandleHeartAnimation;
    }

    private void HandleHeartAnimation(int lives)
    {
        _animator.SetTrigger("LivesChanged");
    }
}

