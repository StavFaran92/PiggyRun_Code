using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInfiniteObject : WorldObject
{
    private Renderer _renderer;

    public WorldInfiniteObject()
    {
        SetMover(new PanoramicMover());
    }

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();

        _renderer = GetComponent<Renderer>();
    }

    public Renderer GetRenderer()
    {
        return _renderer;
    }
}
