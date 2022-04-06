using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool Passed { get; private set; }

    public event Action<Checkpoint> OnPassed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMovementController>();
        if (player == null) return;

        Passed = true;

        if (OnPassed != null)
            OnPassed(this);
    }
}
