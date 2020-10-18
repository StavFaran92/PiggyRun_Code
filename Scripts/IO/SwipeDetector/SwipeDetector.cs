using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    public Text text;

    [SerializeField]
    private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private float minDistanceForSwipe = 20f;

    //public event Action<SwipeData> OnSwipe = delegate { };


    private void Awake()
    {
        text.text = "swipe detector start";
    }
    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {

            text.text = "input detected";
            //text.text = "asasasasas";
            if (touch.phase == TouchPhase.Began)
            {
                text.text = "TouchPhase.Began";
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
            }

            //if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
            //{
            //    text.text = "TouchPhase.Moved";
            //    fingerDownPosition = touch.position;
            //    DetectSwipe();
            //}

            if (touch.phase == TouchPhase.Ended)
            {
                text.text = "TouchPhase.Ended";

                fingerUpPosition = touch.position;
                DetectSwipe();
            }
        }
    }

    private void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {
            text.text = "SwipeDistanceCheckMet()";

            if (IsVerticalSwipe())
            {
                text.text = "IsVerticalSwipe()";

                var direction = fingerDownPosition.y - fingerUpPosition.y < 0 ? SwipeDirection.Up : SwipeDirection.Down;

                SendSwipe(direction);
                
            }
            fingerUpPosition = fingerDownPosition;
        }
    }

    private bool IsVerticalSwipe()
    {
        return VerticalMovementDistance() > HorizontalMovementDistance();
    }

    private bool SwipeDistanceCheckMet()
    {
        return VerticalMovementDistance() > minDistanceForSwipe || HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float VerticalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }

    private void SendSwipe(SwipeDirection direction)
    {
        SwipeData swipeData = new SwipeData()
        {
            Direction = direction,
            StartPosition = fingerDownPosition,
            EndPosition = fingerUpPosition
        };
        IOTouchSwipeHandler.mInstance.SwipeDetectorHandle(swipeData);
        //IOTouchSwipeHandler(swipeData);
        text.text = "fingerUpPosition: "+ fingerUpPosition.y + " fingerDownPosition: "+ fingerDownPosition.y;
    }
}

public struct SwipeData
{
    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public SwipeDirection Direction;
}

public enum SwipeDirection
{
    Up,
    Down,
    Left,
    Right
}