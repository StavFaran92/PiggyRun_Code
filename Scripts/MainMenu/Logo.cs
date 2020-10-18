using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();

        Tween tween = transform.DOScale(new Vector3(.7f, .7f, 0), 4);

        tween.OnComplete(() => transform.DOScale(new Vector3(.6f, .6f, 0), 4)
            .OnComplete(() => tween.Restart( )));



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
