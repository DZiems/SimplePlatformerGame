using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform _sprite;
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;
    [SerializeField] private float _travelSpeed = 5f;

    private float _positionPercent;
    private int _direction = 1;
    private float _lerpPercentMultiplierBySpeed;

    private void Start()
    {
        _positionPercent = 0f;
        float dist = Vector3.Distance(_start.position, _end.position);
        _lerpPercentMultiplierBySpeed = _travelSpeed / dist;
    }

    void Update()
    {
        MoveHorizontally();
    }

    private void MoveHorizontally()
    {

        _positionPercent += Time.deltaTime * _direction * _lerpPercentMultiplierBySpeed;

        _sprite.position = Vector3.Lerp(_start.position, _end.position, _positionPercent);

        if (_positionPercent >= 1 && _direction == 1)
            _direction = -1;
        else if (_positionPercent <= 0 && _direction == -1)
            _direction = 1;
    }

}
