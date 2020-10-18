using Assets.Scripts.WorldObjects.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : WorldFiniteObject, IEnemy
{

    private Animator mAnimator;

    protected new void Start()
    {
        base.Start();

        mAnimator = gameObject.GetComponent<Animator>();
    }

    public void EnemyDie()
    {
        mAnimator.SetBool("explode", true);
    }

    //IEnumerator Die()
    //{

    //}
}
