using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundObject : WorldInfiniteObject
{ 

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

        Init();
    }
}