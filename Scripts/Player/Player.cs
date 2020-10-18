using Assets.Scripts;
using Assets.Scripts.Commands;
using Assets.Scripts.Services;
using Assets.Scripts.StateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, ICharacter, IReceiver//, IColliderListener
{
    internal JumpingState jump;
    //internal ChargingState charge;
    internal DescendingState descend;
    internal UltraJumpState ultraJump;
    internal RunningState run;
    internal SlideState slide;
    internal PowerDescendState powerDescend;
    internal DyingState die;

    public const float jumpDefaultHeight = 1600;
    public const float jumpChargeGain = 500;
    private float mSpeed = 10;

    public const int mAngularDrag = 700;
    private const  float ultraJumpHeight = 1600;
    private readonly Vector3 Vector2Right_Down = new Vector2(1, -1);

    

    private GameObject mStandCollider;
    private GameObject mSlideCollider;
    private GameObject mPlatformCollider;
    

    private int worldIndex;
    private SubScene m_Scene;
    private StateMachine _stateMachine;
    public ParticleSystem dust;
    private Animator mAnimator;
    [HideInInspector]
    public Rigidbody2D mRigidBody2D;

    private HUD mScore;

    private LocalLevelBuilder_v2 mLocalLevelBuilder;

    

    private PlayerSprite mPlayerSprite;

    

    public float m_PlayerCharge;
    [HideInInspector]
    public float m_ChargeAmount = 4;

    private Collider2D collider;
    private GroundObject mGround;

    [HideInInspector]public bool isShieldActive;
    private bool mIsDead;

    enum PlayerAnimation
    {
        PlayerIdle = 0,
        PlayerRunning = 1,
        PlayerJumping = 2,
        PlayerDescending = 3,
        PlayerDying = 4,
        PlayerUltraJump = 5,
        PlayerSliding = 6
    }

    internal bool IsUnderSolidTerrain()
    {
        Debug.Log("Player: IsUnderSolidTerrain()");

        Vector3 dir = Vector3.up;
        //bool cast = Physics.BoxCast(transform.position, new Vector3(1, 1), dir);        

        Vector3 pos1 = transform.position + new Vector3(-1, 1);
        RaycastHit2D hit1 = Physics2D.Raycast(pos1, dir);
        if (hit1.collider != null && hit1.collider.gameObject.CompareTag("SolidTerrain"))
        {
            return true;
        }

        Vector3 pos2 = transform.position + new Vector3(0, 1);
        RaycastHit2D hit2 = Physics2D.Raycast(pos2, dir);
        if (hit2.collider != null && hit2.collider.gameObject.CompareTag("SolidTerrain"))
        {
            return true;
        }

        Vector3 pos3 = transform.position + new Vector3(1, 1);
        RaycastHit2D hit3 = Physics2D.Raycast(pos3, dir);
        if (hit3.collider != null && hit3.collider.gameObject.CompareTag("SolidTerrain"))
        {
            return true;
        }
            
        
        return false;
    }

    private void Awake()
    {
        m_Scene = gameObject.GetComponentInParent(typeof(SubScene)) as SubScene;
        m_Scene.SetPlayer(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _stateMachine = new StateMachine();

        mStandCollider = transform.Find("Stand Collider").gameObject;
        mSlideCollider = transform.Find("Slide Collider").gameObject;
        mPlatformCollider = transform.Find("Platform Collider").gameObject;

        //ColliderBridge cb = mPlatformCollider.AddComponent<ColliderBridge>();
        //cb.Initialize(this);

        //This supposed to fix the raycast issue
        Physics2D.queriesStartInColliders = false;
        collider = GetComponent<Collider2D>();

        run = new RunningState(this, _stateMachine);
        //charge = new ChargingState(this, _stateMachine);
        jump = new JumpingState(this, _stateMachine);
        descend = new DescendingState(this, _stateMachine);
        ultraJump = new UltraJumpState(this, _stateMachine);
        powerDescend = new PowerDescendState(this, _stateMachine);
        slide = new SlideState(this, _stateMachine);
        die = new DyingState(this, _stateMachine);

        mRigidBody2D = GetComponent<Rigidbody2D>();

        //AudioManager.instance.PlayMusicInLoop("footsteps");

        worldIndex = m_Scene.GetIndex();

        mScore = m_Scene.GetScore();

        isShieldActive = false;
        mIsDead = false;



        //PlayerMovementManager.SetInstance(worldIndex, this);

        mPlayerSprite = gameObject.GetComponentInChildren<PlayerSprite>();


        mAnimator = GetComponentInChildren<Animator>();

        //DontDestroyOnLoad(mAnimator);


        mAnimator.SetInteger("State", (int)PlayerAnimation.PlayerRunning);

        _stateMachine.Initialize(run);

        //--Sign as a listener to swipe down event
        m_Scene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_IO_SWIPE_DOWN, Instance_onSwipeDownEvent);
        m_Scene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_IO_TAP, Instance_onSwipeUpEvent);
        m_Scene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_FREEZE, OnCollectedFreeze);
        m_Scene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_SHIELD, OnCollectedShield);

        m_Scene.GetServiceManager().CallActionWhenServiceIsLive(ApplicationConstants.SERVICE_LOCAL_LEVEL_BUILDER, ()=>
        {
            mLocalLevelBuilder = m_Scene.GetServiceManager().GetService<LocalLevelBuilder_v2>(ApplicationConstants.SERVICE_LOCAL_LEVEL_BUILDER);
        });
    }

    private void OnCollectedShield(string arg1, ActionParams arg2)
    {
        m_Scene.GetEventManager().RemoveListenerFromEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_SHIELD, OnCollectedShield);
        m_Scene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_SHIELD_OVER, OnShieldOver);

        isShieldActive = true;
    }

    private void OnShieldOver(string arg1, ActionParams arg2)
    {
        m_Scene.GetEventManager().RemoveListenerFromEvent(ApplicationConstants.EVENT_SHIELD_OVER, OnShieldOver);
        m_Scene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_SHIELD, OnCollectedShield);

        isShieldActive = false;
    }

    private void OnCollectedFreeze(string arg1, ActionParams arg2)
    {
        m_Scene.GetEventManager().RemoveListenerFromEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_FREEZE, OnCollectedFreeze);
        m_Scene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_FREEZE_OVER, OnFreezeOver);

        mAnimator.speed = 0;
    }

    private void OnFreezeOver(string arg1, ActionParams arg2)
    {
        m_Scene.GetEventManager().RemoveListenerFromEvent(ApplicationConstants.EVENT_FREEZE_OVER, OnFreezeOver);
        m_Scene.GetEventManager().ListenToEvent(ApplicationConstants.EVENT_PLAYER_COLLECTED_FREEZE, OnCollectedFreeze);

        mAnimator.speed = 1;

    }

    private void Instance_onSwipeUpEvent(string eventName, ActionParams actionParams)
    {
        _stateMachine.TryPerform(PRActions.TOUCH_SWIPE_UP);
    }

    private void Instance_onSwipeDownEvent(string eventName, ActionParams actionParams)
    {
        _stateMachine.TryPerform(PRActions.TOUCH_SWIPE_DOWN);
    }

    public void UltraJumpDecorate()
    {
        mPlayerSprite.transform.Rotate(Vector3.back * mAngularDrag * Time.deltaTime, Space.Self);

        
    }

    internal void OnCollideWithEnemy()
    {
        if (_stateMachine.GetState().Equals(run))
        {
            if (isShieldActive)
            {
                m_Scene.GetLocalPowerUpManager().DeactivateEffect();
                isShieldActive = false;
            }
            else
            {
                PlayerEnterDieFromHitState();
            }

            //CommonRequests.RequestGameOver();
        }
    }


    private void OnDestroy()
    {
        mAnimator = null;
    }

    // Update is called once per frame
    void Update()
    {
        _stateMachine.UpdateState();
        //int layer_mask = LayerMask.GetMask("Hole");

            //Vector3 dir = Vector3.down;
            ////RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 10, layer_mask);
            //RaycastHit2D hit = Physics2D.Raycast(transform.position, dir);
            //if (hit.collider != null)
            //{

            //    if (hit.collider.gameObject.CompareTag("Hole"))
            //    {
            //        //Debug.DrawLine(transform.position, hit.point, Color.red);
            //        //Debug.Log("Hit Hole");
            //        mPlatformCollider.SetActive(false);
            //        //collider.isTrigger = true;

            //    }

            //    if (hit.collider.gameObject.CompareTag("ground") ||
            //        hit.collider.gameObject.CompareTag("Platform"))
            //    {
            //        //collider.isTrigger = false;
            //        mPlatformCollider.SetActive(true);
            //    }



            //}


        if (!mIsDead)
        {
            transform.position += new Vector3(mSpeed, 0) * Time.deltaTime;

            ShouldPlayerDieByFall();
        }

        


    }

    private void PlayerDie()
    {
        GameEventManager.Instance.CallEvent(ApplicationConstants.EVENT_PLAYER_DIE, ActionParams.EmptyParams);

        mIsDead = true;

        Debug.Log("Player " + worldIndex + " died");
    }

    private void PlayerDieByFall()
    {
        m_Scene.GetEventManager().CallEvent(ApplicationConstants.EVENT_PLAYER_DIE_BY_FALL, ActionParams.EmptyParams);
        mRigidBody2D.isKinematic = true;
        mRigidBody2D.velocity = Vector3.zero;

        PlayerDie();
    }

    private void ShouldPlayerDieByFall()
    {
        if (transform.localPosition.y < -14)
        {
            PlayerDieByFall();
        }
    }

    void CreateDust()
    {
        dust.Play();
    }

    internal void PlayerEnterSlide()
    {
        mAnimator.SetInteger("State", (int)PlayerAnimation.PlayerSliding);

        AudioManager.instance.PlaySoundOverlap("slide");

        mSlideCollider.SetActive(true);
        mStandCollider.SetActive(false);

        GameEventManager.Instance.CallEvent(ApplicationConstants.EVENT_PLAYER_SLIDE, ActionParams.EmptyParams);
    }

    public void PlayerEnterDescend()
    {
        mAnimator.SetInteger("State", (int)PlayerAnimation.PlayerDescending);
    }

    internal void PlayerEnterPowerDescend()
    {
        mPlayerSprite.transform.rotation = Quaternion.identity;
        mAnimator.SetInteger("State", (int)PlayerAnimation.PlayerDescending);

        mRigidBody2D.velocity = Vector2.zero;
        mRigidBody2D.AddForce(Vector2.down * 2000);
    }

    public void PlayerEnterJump()
    {
        //--Handle visuals
        mAnimator.SetInteger("State", (int)PlayerAnimation.PlayerJumping);

        //--Handle sounds
        AudioManager.instance.PlaySoundOverlap("jump");

        //--Handle physics
        mRigidBody2D.AddForce(Vector2.up * jumpDefaultHeight, ForceMode2D.Force);

        //--Handle the event firing
        GameEventManager.Instance.CallEvent(ApplicationConstants.EVENT_PLAYER_JUMP, ActionParams.EmptyParams);
        //GameEventManager.Instance.PlayerJumpEvent();

        //m_Scene.GetEventManager().ListenToEvent("", PlayerEnterUltraJump);
    }

    public void PlayerEnterUltraJump()
    {

         mAnimator.SetInteger("State", (int)PlayerAnimation.PlayerUltraJump);

        AudioManager.instance.PlaySoundOverlap("jump");
        mRigidBody2D.velocity = Vector3.zero;
         mRigidBody2D.AddForce(transform.up * ultraJumpHeight, ForceMode2D.Force);
        mPlayerSprite.ActivateTrail();

        GameEventManager.Instance.CallEvent(ApplicationConstants.EVENT_PLAYER_DOUBLE_JUMP, ActionParams.EmptyParams);

    }

    public void PlayerEnterRun()
    {
        mAnimator.SetInteger("State", (int)PlayerAnimation.PlayerRunning);
        mPlayerSprite.transform.rotation = Quaternion.identity;
        mPlayerSprite.DeactivateTrail();

        
    }

    public void PlayerEnterDieFromHitState()
    {
        if (!mIsDead)
        {
            mAnimator.SetInteger("State", (int)PlayerAnimation.PlayerDying);
            mRigidBody2D.velocity = Vector2.zero;
            mRigidBody2D.AddForce(new Vector2(-.7f, 1.5f) * 500);

            mSpeed = 0;

            AudioManager.instance.PlaySoundOverlap("pig_hurt");

            _stateMachine.ChangeState(die);

            //Dispatch Event
            m_Scene.GetEventManager().CallEvent(ApplicationConstants.EVENT_PLAYER_DIE_BY_HIT, ActionParams.EmptyParams);

            PlayerDie();
        }
    }

    internal void PlayerExitSlide()
    {
        mSlideCollider.SetActive(false);
        mStandCollider.SetActive(true);
    }

    public PlayerState GetState()
    {
        return (PlayerState)_stateMachine.GetState();
    }

    public void TryPerform(PRActions acitivty)
    {
        _stateMachine.TryPerform(acitivty);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //if (!isShieldActive) {
        //    if (collision.gameObject.CompareTag("SolidTerrain"))
        //    {
        //        PlayerEnterDie();

       
        //    }
        //}

        //TODO fix
        if (collision.gameObject.CompareTag("ground") || 
            collision.gameObject.CompareTag("Platform"))
        {
            if (!_stateMachine.GetState().Equals(run) )
            {
                _stateMachine.TryPerform(PRActions.PLAYER_HIT_GROUND);
                CreateDust();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.CompareTag("HoleBottom"))
        //{
        //    AudioManager.instance.PlaySoundOverlap("fall");
        //    //Debug.Log("Game Over!");
        //    GameEventManager.Instance.CallEvent(ApplicationConstants.EVENT_PLAYER_DIE_BY_FALL, ActionParams.EmptyParams);
        //}

        if (collision.gameObject.CompareTag("LevelEnd"))
        {
            Debug.Log("Player " + worldIndex + " Finished!");

            GameEventManager.Instance.CallEvent(ApplicationConstants.EVENT_PLAYER_FINISHED_LEVEL, ActionParams.EmptyParams);

            mSpeed = 0;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground") ||
            collision.gameObject.CompareTag("SolidTerrain") ||
            collision.gameObject.CompareTag("Platform"))
        {
            if (mRigidBody2D.velocity.y < 0)
            {
                _stateMachine.TryPerform(PRActions.PLAYER_LEAPING);

            }
        }
    }


}
