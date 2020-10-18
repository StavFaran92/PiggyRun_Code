using Assets.Scripts;
using Assets.Scripts.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CameraStateMachine;

public class CameraController : MonoBehaviour, IService
{

    private float minZoom = 10f;
    private float maxZoom = 13f;
    private float zoomSpeed = 2f;

    private float currentZoom = 10f;

    Camera mCamera;

    private Player mPlayer;
    private CameraStateMachine _stateMachine;

    private int m_CameraUpperBound; 
    private int m_CameraLowerBound;

    private float width;

    private SubScene mScene;

    private void Awake()
    {
        mScene = GetComponentInParent<SubScene>();

        if (mScene == null)
        {
            Debug.Log("Scene cannot be null");
            return;
        }

        mScene.GetServiceManager().RegisterService(ApplicationConstants.SERVICE_CAMERA_CONTROLLER, this);
    }

    // Start is called before the first frame update
    void Start()
    {
        mCamera = GetComponent<Camera>();

        m_CameraUpperBound = mCamera.pixelHeight * 2 / 3;
        m_CameraLowerBound = mCamera.pixelHeight / 3;

        _stateMachine = new CameraStateMachine();

        mPlayer = GetComponentInParent<SubScene>().GetPlayer();

        float height = mCamera.orthographicSize * 2.0f;
        width = height * mCamera.aspect;
    }



    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 playerPosition = mPlayer.transform.position;
        Vector3 cameraPosition = mCamera.gameObject.transform.position;
        Vector3 playerNormalizedPosition = playerPosition - cameraPosition;
        float playerPositionInCamera = mCamera.pixelHeight / 2 * (playerNormalizedPosition.y / mCamera.orthographicSize + 1) ;

        if (playerPositionInCamera > m_CameraUpperBound && currentZoom < maxZoom)
        {
            _stateMachine.PerformAction(Action.IsAboveUpperBound);

        }

        if (playerPositionInCamera < m_CameraLowerBound && currentZoom > minZoom)
        {
            _stateMachine.PerformAction(Action.IsUnderLowerBound);

        }

        if (playerPositionInCamera < m_CameraUpperBound)
        {
            _stateMachine.PerformAction(Action.IsUnderUpperBound);

        }
        if (currentZoom == minZoom)
        {
            _stateMachine.PerformAction(Action.IsReachMinShrink);
        }

        switch (_stateMachine.GetState())
        {
            case CamState.Expand:
                if (currentZoom < maxZoom)
                {
                    mCamera.transform.position += 
                        new Vector3(zoomSpeed * mCamera.aspect, zoomSpeed, 0) * Time.deltaTime;
                    currentZoom += zoomSpeed * Time.deltaTime; ;
                }
                break;
            case CamState.Shrink:
                if (currentZoom > minZoom)
                {
                    mCamera.transform.position -= 
                        new Vector3(zoomSpeed * mCamera.aspect, zoomSpeed, 0) * Time.deltaTime;
                    currentZoom -= zoomSpeed * Time.deltaTime; ;
                }
                break;

        }

        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);


        //transform.position += new Vector3(10, 0) * Time.deltaTime;
        transform.position = new Vector3(mPlayer.transform.position.x + width / 2 - 5, transform.position.y);

        mCamera.orthographicSize = currentZoom;
    }



    
}

class CameraStateMachine
{

    public enum CamState { DontChange, Expand, Shrink }
    public enum Action { IsUnderLowerBound, IsAboveUpperBound, IsReachMinShrink, IsUnderUpperBound}
    CamState state { get; }
    private Dictionary<CamState, Node> nodesMap =
        new Dictionary<CamState, Node>()
        {
                { CamState.DontChange, new Node(CamState.DontChange) },
                { CamState.Expand, new Node( CamState.Expand) },
                { CamState.Shrink, new Node( CamState.Shrink) }
        };

    Node currentNode;

    public CameraStateMachine PerformAction(Action action)
    {
        currentNode.GetNeighbors().TryGetValue(action, out Node node);
        if(node != null)
            currentNode = node;
        return this;
    }

    public CamState GetState()
    {
        return currentNode.GetState();
    }

    public CameraStateMachine()
    {
        Init();

    }

    private void Init()
    {

        nodesMap.TryGetValue(CamState.DontChange, out Node nodeDontchange);
        nodesMap.TryGetValue(CamState.Expand, out Node nodeExpand);
        nodesMap.TryGetValue(CamState.Shrink, out Node nodeShrink);

        nodeDontchange.SetNeighbors(new Dictionary<Action, Node>
            {
                {Action.IsAboveUpperBound, nodeExpand },
                {Action.IsUnderLowerBound, nodeShrink }
            });

        nodeExpand.SetNeighbors(new Dictionary<Action, Node>
            {
                {Action.IsUnderUpperBound, nodeDontchange }
            });

        nodeShrink.SetNeighbors(new Dictionary<Action, Node>
            {
                {Action.IsReachMinShrink, nodeDontchange },
                {Action.IsAboveUpperBound, nodeExpand }
            });


        currentNode = nodeDontchange;

    }

    class Node
    {
        private Dictionary<Action, Node> neighbors;
        CamState state { get; }

        public Node(CamState state)
        {
            this.state = state;
        }

        public CamState GetState()
        {
            return state;
        }

        public Dictionary<Action, Node> GetNeighbors()
        {
            return neighbors;
        }

        public void SetNeighbors(Dictionary<Action, Node> neighbors)
        {
            this.neighbors = neighbors;
        }
    }
}
