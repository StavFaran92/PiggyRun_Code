using Assets.Scripts;
using Assets.Scripts.StateMachine;
using Assets.Scripts.SystemAI;
using Assets.Scripts.Tutorial;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSystem : MonoBehaviour
{
    //--Singleton
    public static TutorialSystem Instance;

    [SerializeField]
    public bool IsActive = false;

    //3 as the number of scenes in the game
    [SerializeField]
    public Image[] PlaceHolders = new Image[3];

    private Image ActivePh;

    private int mCurrentState = 0;

    private TutorialStateMachine mTutorialStateMachine;

    [SerializeField]
    private TutorialState[] mStates;

    private void Awake()
    {
        Instance = this;

        DOTween.Init();
    }

    private void Update()
    {
        if (mTutorialStateMachine != null)
        {
            mTutorialStateMachine.UpdateState();
        }
    }

    private void Start()
    {
        ////--The tutorial system cannot operate without a DTM therefore we listen for it to init 
        //GameEventManager.Instance.ListenToEvent(ApplicationConstants.EVENT_DEPLOYMENT_TUBE_MANAGER_FINISH_INIT, Init);
        HideItems();

        int shouldPlay = PlayerPrefs.GetInt("Tutorial", -1);

        if(shouldPlay == -1)
        {
            Debug.Log("could not find tutorial data");
            IsActive = false;
        }
        else
        {
            IsActive = shouldPlay == 1;
        }

        ////todo remove
        //IsActive = true;

        
    }

    public void Activate()
    {
        if (IsActive)
        {
            mTutorialStateMachine = new TutorialStateMachine();

            Initialize();

            Operate();
        }
    }

    // Start is called before the first frame update
    public void Init(string eventName)
    {
        
    }

    private void Operate()
    {
        //
        mTutorialStateMachine.Start();
    }

    /// <summary>
    /// Here we initiliazr all the tutorial states
    /// </summary>
    private void Initialize()
    {
        //HideItems();

        if (mStates.Length < 1)
        {
            Debug.Log("Tutorial event list is empty!");
            return;
        }
        mTutorialStateMachine.Initialize(mStates[mCurrentState]);
        Debug.Log("TutorialSystem : Init()");
    }

    private void HideItems()
    {
        foreach (Image image in PlaceHolders)
        {
            image.gameObject.SetActive(false);

        }
    }

    public void MoveToNexTStep()
    {
        mCurrentState++;

        if (mStates.Length > mCurrentState)
        {
            mTutorialStateMachine.ChangeState(mStates[mCurrentState]);
            Debug.Log(String.Format("Tutorial step {0} starts", mCurrentState));
        }
        else
        {
            Debug.Log("End of tutorial");

            PlayerPrefs.SetInt("Tutorial", 0);

            GameEventManager.Instance.CallEvent(ApplicationConstants.EVENT_END_OF_TUTORIAL, ActionParams.EmptyParams);
        }
    }

    internal void HideImage()
    {
        
    }

    public void DisplayImageOnPlaceHolder(Sprite image, int index, string eventName
        , float time = 0, float offset = 0)
    {
        if (index > PlaceHolders.Length)
        {
            Debug.Log("wrong index to display");
            return;
        }

        HideItems();

        PlaceHolders[index].sprite = image;

        StartCoroutine(DisplayImageOnPlaceHolder(time, index, eventName));
    }

    void HideImageWithTween(string eventName, ActionParams actionParams)
    {
        //--Remove listener
        GameEventManager.Instance.RemoveListenerFromEvent(eventName, HideImageWithTween);

        //--Hide the current tutorial image
        if (ActivePh == null)
        {
            Debug.Log("[Tutorial] - active ph is null");
            return;
        }

        ActivePh.transform.DOScale(0, .5f);

    }

    IEnumerator DisplayImageOnPlaceHolder(float time, int index, string eventName)
    {
        yield return new WaitForSeconds(time);

        ActivePh = PlaceHolders[index];

        ActivePh.gameObject.SetActive(true);
        ActivePh.transform.localScale = Vector3.zero;
        ActivePh.transform.DOScale(1, .5f).OnComplete(()=> {

            ActivePh.transform.DOMoveY(ActivePh.transform.position.y+ 40, 1).SetLoops(-1, LoopType.Yoyo);
        });
        GameEventManager.Instance.ListenToEvent(eventName, HideImageWithTween);
        


    }
}
