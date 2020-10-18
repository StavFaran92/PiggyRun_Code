using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.IO
{
    public class SwipeHandler : MonoBehaviour, ISwipeHandler
    {
        private int GetSelectedScene(Vector2 position)
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

            if (data.Direction.Equals(SwipeDirection.Tap))
            {

                //eventManager.SwipeUpEvent();
                eventManager.CallEvent(ApplicationConstants.EVENT_IO_TAP, ActionParams.EmptyParams);
                //eventManager.SwipeUpEvent();
                //eventManager.CallEvent("onSwipeUpEvent");
            }

        }
    }
}
