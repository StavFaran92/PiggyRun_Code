using Assets.Scripts;
using Assets.Scripts.Services;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    AsyncOperation asyncOperation;
    bool startLoading = false;

    [SerializeField]
    private Transform mSettings;
    [SerializeField]  private Transform mTutorial;
    [SerializeField] private Image mImageDark;


    [SerializeField] private ButtonSlide mMusicBtn;
    [SerializeField] private ButtonSlide mSoundBtn;

    private void Start()
    {
        DOTween.Init();

        ////Begin to load the Scene you specify
        //asyncOperation = SceneManager.LoadSceneAsync("Scene Freerun");
        ////Don't let the Scene activate until you allow it to
        //asyncOperation.allowSceneActivation = false;
        //Debug.Log("Pro :" + asyncOperation.progress);

        AudioManager.instance.PlayMusicInLoop("Purple Planet Music - Animal Magic");

        //Preset the Sounds
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            TurnSoundOn(false);
        }
        else
        {
            TurnSoundOff(false);
        }

        //Preset the music
        if (PlayerPrefs.GetInt("Music", 1) == 1)
        {
            TurnMusicOn(false);
        }
        else
        {
            TurnMusicOff(false);
        }

        mSettings.gameObject.SetActive(false);
        //GameEventManager.Instance.ListenToEvent(ApplicationConstants.AUDIO_MANAGER_IS_INITIALIZES, (string eventName, ActionParams actionParams)=>
        //{

        //});
    }

    //private void Update()
    //{
    //    if (!startLoading)
    //    {
    //        LoadScene();
    //        startLoading = true;
    //    }
    //}

    //void LoadScene()
    //{

    //}

    public void OnPlayWithTutorialPressed()
    {
        PlayerPrefs.SetInt("Tutorial", 1);

        LoadPlayScene();
    }

    public void OnPlayWithoutTutorialPressed()
    {
        PlayerPrefs.SetInt("Tutorial", 0);

        LoadPlayScene();
    }

    void LoadPlayScene()
    {
        mImageDark.gameObject.SetActive(true);
        mImageDark.DOFade(1, 1f);
        //mImageDark.transform.Find("Text loading").GetComponent<TextMeshProUGUI>().DOFade(1, 1);

        SceneManager.LoadSceneAsync("Scene Freerun");
    }

    public void OnPlayPressed()
    {
        Debug.Log("Start Free run!");

        AudioManager.instance.PlaySoundOverlap("btn_click");
        mTutorial.gameObject.SetActive(true);
        mTutorial.localScale = Vector3.zero;
        mTutorial.DOScale(Vector3.one, .5f);
        
        
    }

    public void OnShopPressed()
    {
        AudioManager.instance.PlaySoundOverlap("btn_click");
        Debug.Log("Start shop!");
    }

    public void OnSettingsPressed()
    {
        Debug.Log("Start settings!");

        AudioManager.instance.PlaySoundOverlap("btn_click");
        mSettings.gameObject.SetActive(true);
        mSettings.localScale = Vector3.zero;
        mSettings.DOScale(Vector3.one, .5f);
    }

    public void OnSettingsClose()
    {
        AudioManager.instance.PlaySoundOverlap("btn_click");
        mSettings.DOScale(Vector3.zero, .5f)
            .OnComplete(()=> mSettings.gameObject.SetActive(false));
    }

    public void OnLeaderboardPressed()
    {
        AudioManager.instance.PlaySoundOverlap("btn_click");
        Debug.Log("Start leaderboard!");
    }


    /////////////////////////////Music////////////////////////////
    public void TurnMusicOn(bool shouldPlayClick = true)
    {
        PlayButtonClick(shouldPlayClick);
        Debug.Log("TurnMusicOn");

        
        AudioManager.instance.SetMusic(true);


        mMusicBtn.TurnOn();
    }

    public void TurnMusicOff(bool shouldPlayClick = true)
    {
        PlayButtonClick(shouldPlayClick);
        Debug.Log("TurnMusicOff");

        
        AudioManager.instance.SetMusic(false);

        mMusicBtn.TurnOff();
    }

    /////////////////////////////Sound////////////////////////////
    public void TurnSoundOn(bool shouldPlayClick = true)
    {
        
        Debug.Log("TurnSoundOn");

        AudioManager.instance.SetSound(true);

        mSoundBtn.TurnOn();

        PlayButtonClick(shouldPlayClick);
    }

    public void TurnSoundOff(bool shouldPlayClick = true)
    {
        
        Debug.Log("TurnSoundOff");

        AudioManager.instance.SetSound(false);

        mSoundBtn.TurnOff();

        PlayButtonClick(shouldPlayClick);
        
    }

    private void PlayButtonClick(bool shouldPlayClick)
    {
        if (shouldPlayClick)
            AudioManager.instance.PlaySoundOverlap("btn_click");
    }
}
