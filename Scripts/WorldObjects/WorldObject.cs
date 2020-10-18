using Assets.Scripts;
using Assets.Scripts.PowerUps;
using Assets.Scripts.WorldObjects;
using System;
using System.Collections;
using UnityEngine;

public abstract class WorldObject : MonoBehaviour
{
    public float LocalSpeed = 1;

    protected float _worldSpeed = 0;
    protected SubScene m_Scene;
    protected IMover _mover;
    protected string _poolType;
    protected int worldIndex;
    protected IPowerUpHandler mPowerUpHandler;
    protected bool mIsRendered;

    protected void Awake()
    {
        m_Scene = gameObject.GetComponentInParent<SubScene>();
    }

    private void OnBecameVisible()
    {
        mIsRendered = true;
    }

    private void OnBecameInvisible()
    {
        mIsRendered = false;
    }

    internal bool IsRendered()
    {
        return mIsRendered;
    }

    protected void Start()
    {
     

        PrefabTracker tracker = GetComponent<PrefabTracker>();
        if (tracker)
            _poolType = tracker.getPath();
    }

    public virtual void Init()
    {
        m_Scene = gameObject.GetComponentInParent(typeof(SubScene)) as SubScene;

        if (m_Scene == null)
        {
            throw new System.Exception("Object must be attached to a specific scene!");
        }

        float speed = m_Scene.GetWorldSpeed();
        SetWorldSpeed(speed);

        worldIndex = m_Scene.GetIndex();

        LocalPowerUpManager localPowerUpManager = m_Scene.GetLocalPowerUpManager();
        PowerUpManager.PowerUpEffect effect = m_Scene.GetLocalPowerUpManager().GetActivatedPowerUp();
        ActionParams actionParams = localPowerUpManager.GetActionParams();

        if (effect.Equals(PowerUpManager.PowerUpEffect.Speed))
        {
            OnCollectedSpeed("", actionParams);
        }
        else if (effect.Equals(PowerUpManager.PowerUpEffect.Freeze))
        {
            //Nothing is supposed to be created during freeze but just in case
            OnCollectedFreeze("", actionParams);
        }

        //mPowerUpHandler = PowerUpHandlerBase.powerUpDefaultHandler;

        //Listen to events
        m_Scene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_PLAYER_DIE_BY_HIT, OnGameOver);
        m_Scene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_SPEED, OnCollectedSpeed);
        m_Scene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_FREEZE, OnCollectedFreeze);


    }

    private void OnCollectedFreeze(string arg1, ActionParams arg2)
    {
        m_Scene.GetEventManager().RemoveListenerFromEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_FREEZE, OnCollectedFreeze);
        m_Scene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_FREEZE_OVER, OnFreezeOver);

        SetWorldSpeed(0);
    }

    private void OnFreezeOver(string arg1, ActionParams arg2)
    {
        m_Scene.GetEventManager().RemoveListenerFromEvent(ApplicationConstants.EVENT_FREEZE_OVER, OnFreezeOver);
        m_Scene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_FREEZE, OnCollectedFreeze);

        SetWorldSpeed(m_Scene.GetWorldSpeed());
    }

    private void OnCollectedSpeed(string eventName, ActionParams actionParams)
    {
        m_Scene.GetEventManager().RemoveListenerFromEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_SPEED, OnCollectedSpeed);
        m_Scene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_SPEED_OVER, OnSpeedOver);


        float speed = actionParams.Get<float>("speed");

        SetWorldSpeed(speed);
    }

    private void OnSpeedOver(string arg1, ActionParams arg2)
    {
        m_Scene.GetEventManager().RemoveListenerFromEvent(ApplicationConstants.EVENT_SPEED_OVER, OnSpeedOver);
        m_Scene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_SPEED, OnCollectedSpeed);


        SetWorldSpeed(m_Scene.GetWorldSpeed());
    }

    private void OnGameOver(string eventName, ActionParams actionParams)
    {
        m_Scene.GetEventManager().RemoveListenerFromEvent(ApplicationConstants.EVENT_PLAYER_DIE_BY_HIT, OnGameOver);

        SetWorldSpeed(0);
    }

    public SubScene GetScene()
    {
        if (m_Scene == null)
            throw new Exception("Scene cannot be null!");

        return m_Scene;
    }

    protected void Update()
    {
        //_mover.MoveRelativeToWorld(this);

        

        //ShouldDestroy();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EndOfScene"))
        {
            DestroyObject();
        }
    }

    public virtual void DestroyObject()
    {
        //Debug.Log("world object DestroyObject: I was called on "+gameObject.name);

        if (m_Scene != null)
        {
            m_Scene.GetEventManager().RemoveListenerFromEvent(ApplicationConstants.EVENT_PLAYER_DIE_BY_HIT, OnGameOver);
        }



        if (_poolType == null)
        {
            Destroy(gameObject);
            //Debug.Log("This should not happend!" + gameObject.name);
        }
        else
        {
            ObjectPoolManager.Instance.Destroy(_poolType, gameObject);
        }
        
    }


    //protected virtual bool IsObjectLeftOfLeftBound(float screenWidth)
    //{
    //    return transform.position.x * 2 + transform.localScale.x < -screenWidth - 1;
    //}

    public float GetRelativeSpeed()
    {
        return LocalSpeed * _worldSpeed;
    }

    public void SetMover(IMover mover)
    {
        _mover = mover;
    }

    

    public void SetWorldSpeed(float speed)
    {
        _worldSpeed = speed;
    }

    public void SetLocalSpeed(float speed)
    {
        LocalSpeed = speed;
    }

    //internal void HandlePowerUpEffect(PowerUpManager.PowerUpEffect effect, bool value)
    //{
    //    if (mPowerUpHandler == null)
    //    {
    //        throw new Exception("PowerupHandler cannot be null");
    //    }

    //    if (value)
    //        mPowerUpHandler.StartEffect(effect);
    //    else
    //        mPowerUpHandler.StopEffect(effect);
    //}
}

public interface IMover
{
    void MoveRelativeToWorld(WorldObject wo);
}

public class TransormationMover : IMover
{
    public void MoveRelativeToWorld(WorldObject wo)
    {
        wo.transform.position -= new Vector3(wo.GetRelativeSpeed() * Time.deltaTime, 0);
    }
}

public class PanoramicMover : IMover
{
    public void MoveRelativeToWorld(WorldObject wo)
    {
        if (!wo is WorldInfiniteObject)
        {
            throw new System.Exception("Panoramic mover should only be used on infinite objects!");
        }

        Vector2 offset =  CalculateOffset(wo);
         
        ((WorldInfiniteObject)wo).GetRenderer().material.mainTextureOffset += offset * Time.deltaTime; 

    }

    private float ConvertShaderSpeedToWorldSpeed(float speed, int numOfTiles, float spriteWidth)
    {
        return speed * numOfTiles / spriteWidth;
    }

    Vector2 CalculateOffset(WorldObject wo)
    {
        Renderer renderer = ((WorldInfiniteObject)wo).GetRenderer();



        return new Vector2( ConvertShaderSpeedToWorldSpeed(
            wo.GetRelativeSpeed(),
            (int) Mathf.Floor(renderer.material.mainTextureScale.x),
            renderer.localToWorldMatrix.lossyScale.x), 0);
    }
}