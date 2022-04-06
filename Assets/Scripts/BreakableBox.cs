using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBox : MonoBehaviour, ITakeShellHits
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!(collision.WasHitByPlayer() && collision.WasHitFromBelow()))
            return;

        Destroy(gameObject);
    }


    public void HandleShellHit(ShellFlipped shellFlipped)
    {
        Destroy(gameObject);
    }


}

