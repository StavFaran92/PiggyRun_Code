using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public abstract class AbstractIOHandler : MonoBehaviour
    {
        public Text text;

        int GetSelectedScene(Vector2 position)
        {
            return (int)Math.Floor(Math.Max(0, Math.Min(Screen.height,
                position.y * 3 / Screen.height)));
        }

        public void SwipeDetectorHandle(SwipeData data)
        {
            //text.text = "SwipeDetector_OnSwipe";
            int selectedSubScene = GetSelectedScene(data.StartPosition);

            SubScene subscene = GameManager.GetInstance().GetSubScene(selectedSubScene);
            if (subscene == null)
            {
                Debug.Log("subscene not found, check your selectedSubScene index");
                return;
            }
            SceneEventManager eventManager = subscene.GetEventManager();
            if (eventManager == null)
            {
                Debug.Log("eventManager not found in scene");
                return;
            }

            if (data.Direction.Equals(SwipeDirection.Down))
            {

                //eventManager.SwipeDownEvent();
                eventManager.CallEvent(ApplicationConstants.EVENT_IO_SWIPE_DOWN, ActionParams.EmptyParams);
            }

            if (data.Direction.Equals(SwipeDirection.Up))
            {

                //eventManager.SwipeUpEvent();
                eventManager.CallEvent(ApplicationConstants.EVENT_IO_SWIPE_UP, ActionParams.EmptyParams);
                //eventManager.SwipeUpEvent();
                //eventManager.CallEvent("onSwipeUpEvent");
            }

        }
    }
}