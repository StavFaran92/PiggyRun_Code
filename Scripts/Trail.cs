using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    //TODO Optimize!!!
    private bool _isActive;

    private SubScene mScene;
    private Vector3 mScale;

    private void Start()
    {
        mScene = gameObject.GetComponentInParent(typeof(SubScene)) as SubScene;
        Player player = mScene.GetPlayer();
        mScale = player.transform.localScale;
    }


    public void Activate()
    {
        _isActive = true;
        InvokeRepeating("SpawnTrail", 0, 0.2f); // replace 0.2f with needed repeatRate
    }

    public void Deactivate()
    {
        _isActive = false;
        CancelInvoke("SpawnTrail");
    }

    public bool IsActive()
    {
        return _isActive;
    }

    void SpawnTrail()
    {
        GameObject trailPart = new GameObject();
        trailPart.transform.SetParent(mScene.transform);
        trailPart.transform.localScale = mScale;
        SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();
        trailPart.AddComponent<WorldFiniteObject>().SetLocalSpeed(1);

        trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
        trailPart.transform.position = transform.position;
        Destroy(trailPart, 0.5f); // replace 0.5f with needed lifeTime

        StartCoroutine("FadeTrailPart", trailPartRenderer);
    }

    IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
    {
        Color color = trailPartRenderer.color;
        color.a -= 0.5f; // replace 0.5f with needed alpha decrement
        trailPartRenderer.color = color;

        yield return new WaitForEndOfFrame();
    }
}
