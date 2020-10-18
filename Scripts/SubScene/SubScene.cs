using Assets.Scripts;
using Assets.Scripts.Commands;
using Assets.Scripts.PowerUps;
using Assets.Scripts.PowerUps.Effects;
using Assets.Scripts.SceneData;
using Assets.Scripts.SequenceCommand;
using Assets.Scripts.Services;
using Assets.Scripts.StateMachine;
using Assets.Scripts.SystemAI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class SubScene : MonoBehaviour, IActor
{
    private Camera m_Camera;
    private AbstractIOHandler m_IO;
    private HUD m_Score;
    private GroundObject m_Ground;

    private GameManager m_GameManager;

    private GameCinematics mGameCinematics;

    private SceneEventManager mSceneEventManager;

    private Player m_Player;

    public readonly Dictionary<ReceiverId, IReceiver> m_ReceiversDict
            = new Dictionary<ReceiverId, IReceiver>();

    private int _sceneHeightInPixels ,_sceneWidthInPixels;

    public float mScreenHeightInWorld { get; internal set; }
    public float mScreenWidthInWorld { get; internal set; }

    private float _sceneSpeed;

    private int _sceneIndex;

    [SerializeField]private LocalPowerUpManager localPowerUpManager;
    private LocalLevelBuilder mLocalLevelBuilder;

    private ServiceManager mServiceManager = new ServiceManager();

    private int mMetersToChangeWorld;
    private float mMetersRanInWorld = 0;

    private int mMinMetersToChangeWorld = 50;
    private int mMaxMetersToChangeWorld = 100;

    // Start is called before the first frame update
    void Awake()
    {
        //todo fix
        _sceneIndex = Convert.ToInt32(gameObject.name.Split(' ')[1]) - 1;

        SubSceneMultiton.SetInstance(_sceneIndex, this);

        mScreenHeightInWorld = Camera.main.orthographicSize * 2.0f;
        mScreenWidthInWorld = mScreenHeightInWorld * Camera.main.aspect;

        _sceneSpeed = 0;

        mSceneEventManager = new SceneEventManager();

        mMetersToChangeWorld = UnityEngine.Random.Range(mMinMetersToChangeWorld, mMaxMetersToChangeWorld);

        //GameObject go = new GameObject();
        //go.transform.parent = transform;
        //localPowerUpManager = go.AddComponent<LocalPowerUpManager>();
        //localPowerUpManager.SetSubScene(this);



    }

    internal ServiceManager GetServiceManager()
    {
        return mServiceManager;
    }

    public void SetLocalLevelBuilder(LocalLevelBuilder levelBuilder)
    {
        if(mLocalLevelBuilder == null)
            mLocalLevelBuilder = levelBuilder;
    }

    internal LocalLevelBuilder GetLocalLevelBuilder()
    {
        return mLocalLevelBuilder;
    }

    private void Start()
    {
        m_GameManager = GameManager.GetInstance();
        m_GameManager.RegisterSceneInGame(this, _sceneIndex);
    }

    public SceneEventManager GetEventManager()
    {
        return mSceneEventManager;
    }

    public float GetWorldSpeed()
    {
        return _sceneSpeed;
    }

    internal void SetScore(HUD score)
    {
        this.m_Score = score;

        localPowerUpManager.SetHUD(m_Score);
    }

    public LocalPowerUpManager GetLocalPowerUpManager()
    {
        return localPowerUpManager;
    }

    internal void SetGround(GroundObject groundInfiniteObject)
    {
        this.m_Ground = groundInfiniteObject;
    }

    public void SetGameCinematics(GameCinematics gameCinematics)
    {
        mGameCinematics = gameCinematics;
    }

    public GameCinematics GetGameCinematics()
    {
        return mGameCinematics;
    }

    internal HUD GetScore()
    {
        return m_Score;
    }

    internal void SetPlayer(Player player)
    {
        this.m_Player = player;
    }

    public Player GetPlayer()
    {
        return m_Player;
    }

    private void Update()
    {
        //TODO fix
        float amount = _sceneSpeed / 10 * Time.deltaTime;

        //--Update view
        m_Score.IncreaseMetersByAmount(amount);

        //--Update logic
        mMetersRanInWorld += amount;
        if(mMetersRanInWorld > mMetersToChangeWorld)
        {
            mMetersToChangeWorld = UnityEngine.Random.Range(mMinMetersToChangeWorld, mMaxMetersToChangeWorld);
            mMetersRanInWorld = 0;


            DeploymentTubeManager.Instance.ApplyRequestToTube(_sceneIndex, (Action action) => {
                GetEventManager().CallEvent(ApplicationConstants.EVENT_CHANGE_TO_NEXT_WORLD, ActionParams.EmptyParams);

                action.Invoke();
            }, this);
        }

        //if(m_Score.GetMeters())
    }

    public int GetIndex()
    {
        return _sceneIndex;
    }

    public void notifyMe()
    {
        //--Ignored
    }

    //public void TryPerform(PRActions activity)
    //{

    //    if (activity.Equals(PRActions.POWERUP_MAGNET))
    //    {
    //        localPowerUpManager.ActivatePowerUpEffect(new PowerUpMagnet());
    //    }
    //}
}
