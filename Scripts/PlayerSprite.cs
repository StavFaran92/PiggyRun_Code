
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    private Trail trail;
    // Start is called before the first frame update
    void Start()
    {
        trail = gameObject.AddComponent<Trail>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void ActivateTrail()
    {
        if (!trail.IsActive())
            trail.Activate();
    }

    internal void DeactivateTrail()
    {
        if(trail)
            trail.Deactivate();
    }
}
