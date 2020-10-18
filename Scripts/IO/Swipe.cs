using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.IO
{
    public class Swipe : MonoBehaviour
    {
        private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
        private bool tapRequested;
        private bool isDragging = false;
        private Vector2 startTouch, swipeDelta;

        [SerializeField]private SwipeHandler mSwipeHandler;

        private void Update()
        {
            tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

            #region Standalone Inputs
            if (Application.isEditor)
            {
                
                if (Input.GetMouseButtonDown(0))
                {
                    tapRequested = true;
                    isDragging = true;
                    startTouch = Input.mousePosition;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    if (tapRequested)
                    {
                        //Debug.Log("Swipe: tap");
                        tap = true;

                        SwipeData swipeData = new SwipeData()
                        {
                            Direction = SwipeDirection.Tap,
                            StartPosition = startTouch
                        };
                        mSwipeHandler.SwipeDetectorHandle(swipeData);
                    }
                    isDragging = false;
                    Reset();
                }
            }
            #endregion

            #region Mobile Inputs
            if (!Application.isEditor)
            {
                if (Input.touchCount > 0)
                {
                    if (Input.touches[0].phase == TouchPhase.Began)
                    {
                        tapRequested = true;
                        isDragging = true;
                        startTouch = Input.touches[0].position;
                    }
                    else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                    {
                        if (tapRequested)
                        {
                            tap = true;
                            //Debug.Log("Swipe: tap");

                            SwipeData swipeData = new SwipeData()
                            {
                                Direction = SwipeDirection.Tap,
                                StartPosition = startTouch
                            };
                            mSwipeHandler.SwipeDetectorHandle(swipeData);
                        }
                        isDragging = false;
                        Reset();
                    }
                }
            }
            #endregion

            //Calculate the distance
            swipeDelta = Vector2.zero;
            if (isDragging)
            {
                if (Input.touchCount > 0) { swipeDelta = Input.touches[0].position - startTouch; }
                else if (Input.GetMouseButton(0)) { swipeDelta = (Vector2)Input.mousePosition - startTouch; }
            }

            //Did we cross the dead zone?
            if (swipeDelta.magnitude > 100)
            {
                tapRequested = false;
                //Which direction are we swiping?
                float x = swipeDelta.x;
                float y = swipeDelta.y;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    //Left or right?
                    if (x > 0) {
                        Debug.Log("Swipe: swipeRight");
                        swipeRight = true;
                    }
                    else {
                        Debug.Log("Swipe: swipeLeft");
                        swipeLeft = true;
                    }
                    //x > 0 ? swipeRight = true : swipeLeft = true;
                }
                else
                {
                    //Up or down?
                    if (y > 0) {
                        //Debug.Log("Swipe: swipeUp");
                        swipeUp = true;
                    }
                    else {
                        //Debug.Log("Swipe: swipeDown");
                        swipeDown = true;
                        SwipeData swipeData = new SwipeData()
                        {
                            Direction = SwipeDirection.Down,
                            StartPosition = startTouch
                        };
                        mSwipeHandler.SwipeDetectorHandle(swipeData);
                    }
                    // y > 0 ? swipeUp = true : swipeDown = true;
                }
                Reset();
            }
        }

        private void Reset()
        {
            startTouch = swipeDelta = Vector2.zero;
            isDragging = false;
        }

        public Vector2 SwipeDelta { get { return swipeDelta; } }
        public bool SwipeLeft { get { return swipeLeft; } }
        public bool SwipeRight { get { return swipeRight; } }
        public bool SwipeUp { get { return swipeUp; } }
        public bool SwipeDown { get { return swipeDown; } }
        public bool Tap { get { return tap; } }
    }

    public struct SwipeData
    {
        public Vector2 StartPosition;
        public SwipeDirection Direction;
    }

    public enum SwipeDirection
    {
        Up,
        Down,
        Left,
        Right,
        Tap
    }
}
