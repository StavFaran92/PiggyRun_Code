using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMenu : MonoBehaviour
{
    [SerializeField]
    private float endValue;

    [SerializeField]
    private float moveDuration;

    [SerializeField]
    private float restartDuration;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();


        Tweener tweener = transform.DOMoveX(endValue, moveDuration);
        tweener.OnComplete(()=>tweener.Restart(true, restartDuration));


    }
}
