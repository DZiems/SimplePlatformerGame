using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGrounding : MonoBehaviour
{

    [SerializeField] private List<Transform> _positions;
    [SerializeField] private float _distanceFromFeetToGround = 0.1f;
    [SerializeField] private LayerMask _layerMask;


    private Transform _groundObject = null;
    private Vector3? _groundObjectLastPosition;

    public bool IsGrounded { get; private set; }
    public Vector2 GroundedDirection { get; private set; }

    private void Update()
    {
        foreach (var transform in _positions)
        {
            CheckFootForGrounding(transform);
            if (IsGrounded) break;
        }

        StickToMovingObjects();
    }

    private void StickToMovingObjects()
    {
        if (_groundObject != null)
        {
            if (_groundObjectLastPosition.HasValue &&
                (_groundObjectLastPosition.Value != _groundObject.position))
            {
                Vector3 delta = _groundObject.position - _groundObjectLastPosition.Value;
                transform.position += delta;
            }
            _groundObjectLastPosition = _groundObject.position;
        }
        else
        {
            _groundObjectLastPosition = null;
        }
    }

    private void CheckFootForGrounding(Transform transform)
    {
        var footRaycastHit = Physics2D.Raycast(transform.position, transform.forward, _distanceFromFeetToGround, _layerMask);
        Debug.DrawRay(transform.position, transform.forward * _distanceFromFeetToGround, Color.red);

        if (footRaycastHit.collider != null)
        {
            if (_groundObject != footRaycastHit.collider.transform)
            {
                _groundObject = footRaycastHit.collider.transform;
                _groundObjectLastPosition = _groundObject.position;
            }
            IsGrounded = true;
            GroundedDirection = transform.forward;
        }
        else
        {
            IsGrounded = false;
        }
    }


}

