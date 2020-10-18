using Assets.Scripts.Services;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSlide : MonoBehaviour
{
    public enum State
    {
        On,
        Off
    }

    private State mCurrentState;
    private float positionStartX;

    private float positionEndX;

    public float offsetX;

    public State initialState;

    public Transform buttonOn;
    public Transform buttonOff;

    // Start is called before the first frame update
    void Awake()
    {
        mCurrentState = initialState;
        positionStartX = buttonOn.position.x;

        positionEndX = positionStartX + offsetX;

        DOTween.Init();


    }

    public void TurnOn()
    {
        if (mCurrentState.Equals(State.Off))
        {
            AudioManager.instance.PlaySoundOverlap("button_click");

            buttonOn.DOMoveX(positionStartX, .3f);
            buttonOff.DOMoveX(positionStartX, .3f);
            buttonOn.gameObject.SetActive(true);
            buttonOff.gameObject.SetActive(false);
            mCurrentState = State.On;
        }
    }

    public void TurnOff()
    {
        if (mCurrentState.Equals(State.On))
        {
            AudioManager.instance.PlaySoundOverlap("button_click");


            buttonOn.transform.DOMoveX(positionEndX, .3f);
            buttonOff.transform.DOMoveX(positionEndX, .3f);
            buttonOn.gameObject.SetActive(false);
            buttonOff.gameObject.SetActive(true);
            mCurrentState = State.Off;
        }
    }

}