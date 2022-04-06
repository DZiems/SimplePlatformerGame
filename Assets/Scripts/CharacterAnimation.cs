using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class CharacterAnimation : MonoBehaviour
{
    private Animator _animator;
    private IMove _mover;
    private SpriteRenderer _spriteRenderer;
    private CharacterGrounding _characterGrounding;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _mover = GetComponent<IMove>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _characterGrounding = GetComponent<CharacterGrounding>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("Speed", Mathf.Abs(_mover.CurrentSpeed));
        if (_mover.CurrentSpeed != 0)
            _spriteRenderer.flipX = _mover.CurrentSpeed < 0;

        _animator.SetBool("Jump", !_characterGrounding.IsGrounded);
    }
}
