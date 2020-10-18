using Assets.Scripts;
using Assets.Scripts.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallIntoHole : MonoBehaviour
{
    private BoxCollider2D mBoxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        mBoxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
