using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WorldFiniteObject : WorldObject
{
    //CountDownLatch child;
    bool mIsSubscribedToRoot = false;
    CountDownRoot mRoot = null;

    public new void Start()
    {
        base.Start();

        SetMover(new TransormationMover());

        //gameObject.
        //if(GetComponentInParent<Grid>() != null)
    }

    public void SubscribeToRoot(CountDownRoot root)
    {
        mRoot = root;
        mRoot.IncreaseByOne();
        mIsSubscribedToRoot = true;
    }

    //public void SetAsChildOfGrid(CountDownRoot root)
    //{
    //    child = new CountDownLatch(root);
    //    //Debug.Log("Added " + gameObject.name);
    //}

    public void DestroyMe()
    {
        DestroyObject();
    }

    public override void DestroyObject()
    {
        gameObject.transform.parent = null;

        if (mIsSubscribedToRoot && mRoot != null)
        {
            mRoot.DecreaseByOne();
            mRoot = null;
            mIsSubscribedToRoot = false;
        }

        //if (child != null)
        //    child.DecreaseByOne();

        

        //child = null;

        base.DestroyObject();
    }

    public new void Update()
    {
        base.Update();

        //if (transform.position.x < - m_Scene.mScreenWidthInWorld / 2)
        //{
        //    DestroyObject();
        //}
    }

    //public void SetSpeed(float speed)
    //{
    //    this._localSpeed = speed;
    //}
}