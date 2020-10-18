using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerObject : WorldFiniteObject
{
    private new void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (collision.gameObject.CompareTag("player"))
        {
            OnPlayerTrigger(collision);
        }

        base.OnTriggerEnter2D(collision);
    }

    public abstract void OnPlayerTrigger(Collider2D collision);
}
