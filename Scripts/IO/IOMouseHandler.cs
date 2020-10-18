using Assets.Scripts.Commands;
using Assets.Scripts.Communication.BluePrints;
using Assets.Scripts.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.Scripts.GameManager;

namespace Assets.Scripts
{
    class IOMouseHandler:  AbstractIOHandler
    {
        //bool isChosen = false;

        //private static IOHandler mInstance = new IOHandler();
        private SubScene m_Subscene;
        private GameManager m_GameManager;

        //private int m_SceneIndex;

        int fingerId;

        //public IOMouseHandler(SubScene subscene) {
            

        //    m_Subscene = subscene;

        //    m_SceneIndex = m_Subscene.GetIndex();
        //}

        public void Start()
        {
            m_GameManager = GameManager.GetInstance();
        }

        //public static IOHandler GetIntance()
        //{
        //    if(mInstance == null)
        //    {
        //        mInstance = new IOHandler();
        //    }
        //    return mInstance;
        //}

        //int GetSelectedScene()
        //{
        //    return (int)Math.Floor(Math.Max(0, Math.Min(Screen.height,
        //        Input.mousePosition.y * 3 / Screen.height)));
        //}

        //bool IsChosenScene()
        //{
        //    bool isChosen = false;
            
        //        float min = Math.Min(Screen.height, Input.mousePosition.y * 3 / Screen.height);
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

        //void IsChosenScene()
        //{
        //    Touch[] touches = Input.touches;
        //    foreach (Touch touch in touches)
        //    {
        //        float min = Math.Min(Screen.height, touch.position.y * 3 / Screen.height);
        //        float max = Math.Max(min, 0);
        //        double floor = Math.Floor(max);
        //        int floor2 = (int)floor;
        //        if (floor2 == m_SceneIndex)
        //        {
        //            isChosen = true;
        //            fingerId = touch.fingerId;
        //        }
        //    }

        //    //todo fix
        //    //return ((double)(Math.Floor(Math.Max(0, Math.Min(Screen.height, Input.mousePosition.y * 3 / Screen.height))) == m_SceneIndex;
        //}

        //public IReceiver GetReceiver(ReceiverId receiver)
        //{
        //    //bool test = true;
        //    //GameManager.mGameReceiversMap.TryGetValue(receiver, out IReceiver[] receivers);

        //    //if (test == true)
        //    //{
        //    //    return receivers[3];
        //    //}



        //    int scene = GetSelectedScene();

        //    if (scene < 0 || scene > 2)
        //        throw new Exception("Scene has to be between 0 - 2");

        //    if (receivers[scene] == null)
        //        throw new Exception("receiver is null!");

        //    return receivers[scene];
        //}

        //IReceiver GetPlayerReceiver()
        //{
        //    IReceiver receiver = 
        //        PlayerMovementManager.Instance.GetInstance(GetSelectedScene());
        //        //T.Instance().GetInstance(GetSelectedScene());
        //        //T.Instance.GetInstance(GetSelectedScene());

        //    if (receiver == null)
        //        throw new Exception("receiver is null!");

        //    return receiver;
        //}

        public void Update()
        {
            ;

            //if (IsChosenScene())
            {
                HandleInput();
            }
        }

        public void HandleInput()
        {


            if (Input.GetKeyDown(KeyCode.D))
            {
                //ICommand command = CommandFactory.CreateCommandCreation<BluePrintItem>()
                //    .WithPosition(Vector3.zero)
                //    .SetElement(new BluePrintHolder<BluePrintItem>)
                //    .SetImageMap("wtf")
                //    .Build();

                //BluePrintConstruct blc = new BluePrintConstruct() { imageMap = "stav" };

                //ICommand command = CommandFactory.CreateCommandCreation<BluePrintConstruct>()
                //    .WithPosition(Vector3.one)
                //    .IsConstruct(new BluePrintHolder<BluePrintConstruct>() { data = blc })
                //    .SetImageMap("wtf")
                //    .Build();

                //BluePrint bluePrint = BluePrintItemBuilder
                //    .Instance
                //    .SetItemType(ItemType.CoinGold)
                //    .Build();

                //ICommand command = CommandFactory.CreateCommandCreation()
                //    .WithPosition(Vector3.one)
                //    .SetBluePrint(bluePrint)
                //    .Build();

                //Request request = Request.CreateRequest()
                //    .WithReceiver(m_GameManager.GetReceiver(ReceiverId.Scenes, GetSelectedScene()))
                //    .WithCommand(command)
                //    .Build();


                //CommandFactory.CreateCommandCreation()
                //    .OfElement(Element.Construct)
                //    .IsConstruct()
                //    .SetImageMap("whatever")
                //    .WithPosition(Vector3.zero)
                //    .Build();

                //Send(request);
            }

            

            if (Input.GetKeyDown(KeyCode.F))
            {
                //_gameManager.DelegateMethod("CreateGround", UnityEngine.Random.Range(0, 3));

                //GenericFactory factory = new GenericFactory();
                //GameObject ground = factory.GetNewInstance("Grounds", new Vector3(13, -3, -15));
                ////GameObject ground = ObjectPoolManager.Instance.Spawn(PoolType.GROUND_POOL, new Vector3(13, -3, -15));
                //ground.transform.localScale = new Vector3(UnityEngine.Random.Range(1, 5), 1, 1);
                //GenericFactory factory = gameObject.AddComponent<GenericFactory>();
                //factory.GetNewInstance(_coinPrefab, new Vector3(Screen.width, 10, -15));
                //gameObjectFactory.CreateCoin(new Vector3(Screen.width, 10, -15));
            }

            if (Input.GetMouseButtonDown(1))
                //if (Input.GetMouseButtonDown(0))
            {
                SwipeData swipeData = new SwipeData()
                {
                    Direction = SwipeDirection.Down,
                    StartPosition = Input.mousePosition,
                    EndPosition = Vector2.zero
                };
                SwipeDetectorHandle(swipeData);


                //ICommand command = CommandFactory.CreateCommandBehaviour()
                //    .WithAction(PRActions.MOUSE_LEFT_BUTTON)
                //    .Build();

                //Request request = Request.CreateRequest()
                //    .WithReceiver(m_GameManager.GetReceiver(ReceiverId.Players, GetSelectedScene()))
                //    .WithCommand(command)
                //    .Build();



                //Send(request);
            }

            if (Input.GetMouseButtonDown(0))

            //if (Input.GetMouseButtonUp(0))
            {
                //isChosen = false;


                SwipeData swipeData = new SwipeData()
                {
                    Direction = SwipeDirection.Up,
                    StartPosition = Input.mousePosition,
                    EndPosition = Vector2.zero
                };
                SwipeDetectorHandle(swipeData);
                //ICommand command = CommandFactory.CreateCommandBehaviour()
                //    .WithAction(PRActions.MOUSE_RIGHT_BUTTON)
                //    .Build();

                //Request request = Request.CreateRequest()
                //    .WithReceiver(m_GameManager.GetReceiver(ReceiverId.Players, GetSelectedScene()))
                //    .WithCommand(command)
                //    .Build();



                //Send(request);
            }

            if (Input.GetKeyDown(KeyCode.Z))

            {

                
                //_gameManager.DelegateMethod("SetSpeed", 15);
            }

            //if (Input.GetKeyDown(KeyCode.A))
            //{
            //    Debug.Log("Start Hole");
            //    //_gameManager.DelegateMethod("CreateHole");
            //}

            if (Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("close hole");
                //_gameManager.DelegateMethod("CloseHole");
            }

        }

        

        public void Send(Request request)
        {
            m_GameManager.SendMessage("AddRequest", request);
        }
    }

    

    interface ISender
    {
        IReceiver GetReceiver(ReceiverId receiver);
        void Send(Request request);
    }


}
