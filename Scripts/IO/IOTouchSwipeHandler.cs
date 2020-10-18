using Assets.Scripts.Commands;
using Assets.Scripts.Communication.BluePrints;
using Assets.Scripts.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Assets.Scripts.GameManager;

namespace Assets.Scripts
{
    class IOTouchSwipeHandler :  AbstractIOHandler
    {
        public enum Actions
        {
            SwipeUp,
            SwipeDown
        }

        #region FIELDS

        

        public static IOTouchSwipeHandler mInstance;

        //private enum DraggedDirection
        //{
        //    Up,
        //    Down,
        //    Right,
        //    Left
        //}
        #endregion

        #region  IDragHandler - IEndDragHandler

        //bool isChosen = false;

        //private static IOHandler mInstance = new IOHandler();
        //private SubScene m_Subscene;
        private GameManager m_GameManager;

        private SceneEventManager mEventManager;

        private int m_SceneIndex;

        int fingerId;

        private void Awake()
        {
            mInstance = this;

            text.text = "IO started";
        }

        public void Start()
        {
            
            

            m_GameManager = GameManager.GetInstance();

            //m_Subscene = GetComponentInParent<SubScene>();

            //m_SceneIndex = m_Subscene.GetIndex();
        }

        //public static IOHandler GetIntance()
        //{
        //    if(mInstance == null)
        //    {
        //        mInstance = new IOHandler();
        //    }
        //    return mInstance;
        //}

        //int GetSelectedScene(Vector2 position)
        //{
        //    return (int)Math.Floor(Math.Max(0, Math.Min(Screen.height,
        //        position.y * 3 / Screen.height)));
        //}

        //bool IsChosenScene(Vector2 position)
        //{
        //    bool isChosen = false;
            
        //        float min = Math.Min(Screen.height, position.y * 3 / Screen.height);
        //        float max = Math.Max(min, 0);
        //        double floor = Math.Floor(max);
        //        int floor2 = (int)floor;
        //        if (floor2 == m_SceneIndex)
        //        {
        //            isChosen = true;
        //        }

        //    return isChosen;
            

        //    //todo fix
        //    //return ((double)(Math.Floor(Math.Max(0, Math.Min(Screen.height, Input.mousePosition.y * 3 / Screen.height))) == m_SceneIndex;
        //}

        //public void Update()
        //{
        //    ;

        //    if (IsChosenScene())
        //    {
        //        HandleInput();
        //    }
        //}

        public void HandleInput()
        {


            

            


        }

        //private void SendSwipeToPlayer(int selectedSubScene, SwipeDirection direction)
        //{
            
        //    PRActions action = PRActions.NULL;
        //    if (direction.Equals(SwipeDirection.Up))
        //    {
        //        action = PRActions.TOUCH_SWIPE_UP;
        //        text.text = "SwipeUp";
        //    }
        //    else if (direction.Equals(SwipeDirection.Down))
        //    {
        //        action = PRActions.TOUCH_SWIPE_DOWN;
        //        text.text = "SwipeDown";
        //    }


        //    ICommand command = CommandFactory.CreateCommandBehaviour()
        //            .WithAction(action)
        //            .Build();

        //    Request request = Request.CreateRequest()
        //        .WithReceiver(m_GameManager.GetReceiver(ReceiverId.Players, selectedSubScene))
        //        .WithCommand(command)
        //        .Build();



        //    Send(request);
        //}

        public void Send(Request request)
        {
            m_GameManager.SendMessage("AddRequest", request);
        }

        ////It must be implemented otherwise IEndDragHandler won't work 
        //public void OnDrag(PointerEventData eventData)
        //{

        //}

        //public void OnEndDrag(PointerEventData eventData)
        //{
        //    Debug.Log("Press position + " + eventData.pressPosition);
        //    Debug.Log("End position + " + eventData.position);
        //    Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
        //    Debug.Log("norm + " + dragVectorDirection);
        //    GetDragDirection(dragVectorDirection);

        //    if (IsChosenScene(eventData.pressPosition))
        //    {
        //        HandleInput();
        //    }
        //}

        //private DraggedDirection GetDragDirection(Vector3 dragVector)
        //{
        //    float positiveX = Mathf.Abs(dragVector.x);
        //    float positiveY = Mathf.Abs(dragVector.y);
        //    DraggedDirection draggedDir;
        //    if (positiveX > positiveY)
        //    {
        //        draggedDir = (dragVector.x > 0) ? DraggedDirection.Right : DraggedDirection.Left;
        //    }
        //    else
        //    {
        //        draggedDir = (dragVector.y > 0) ? DraggedDirection.Up : DraggedDirection.Down;
        //    }
        //    Debug.Log(draggedDir);
        //    return draggedDir;
        //}
        #endregion
    }




}
