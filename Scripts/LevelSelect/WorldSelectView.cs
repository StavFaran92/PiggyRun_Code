using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;
using Assets.Scripts;
using Assets.Scripts.LevelSelect;
using DG.Tweening;

/// <summary>
/// This is the Shop
/// it is a singleton
/// it is responsible of purchases in the game
/// </summary>
public class WorldSelectView : MonoBehaviour
{

    UnityAction<LevelItem> action;
    delegate void MyDelegate(int num);
    MyDelegate myDelegate;

    //-- Singleton
    public static WorldSelectView mInstance;

    //-- Local Database
    Data db;

    #region FIELDS
    //private Transform mContainerWeapons;
    //private Transform mContainerGems;

    private RectTransform mLevelScreenRect;
    private const int mLevelScreenPad = 300;
    private const float mScreenScrollTime = .5f;
    private int mNumberOfWorlds;


    [SerializeField]private GameObject mLevelTemplate;
    [SerializeField]private GameObject mLevelScreenTemplate;
    [SerializeField]private GameObject mStarTemplate;
    [SerializeField]private GameObject mDiamondTemplate;
    //private IShopCustomer mShopCustomer;
    private float mPaddingX = 20;
    private float mPaddingY = 20;

    [SerializeField]private Transform mWorldPh;
    int currentWorldIndex = 0;

    [Header("Temps")]

    private int mColSize = 3;

    #endregion

    #region METHODS

    private void Awake()
    {
        //--Singleton
        mInstance = this;

    }

    private void Start()
    {
        //-- Get store item data from json file
        string jsonData = Utils.LoadResourceTextfile("data/userData");
        db = Utils.ParseJsonData<Data>(jsonData);

        //--Get number of worlds from DB
        mNumberOfWorlds = db.data.Count;

        //--Populate worlds
        for (int i=0; i<db.data.Count; i++)
        {
            //--Create a world container
            GameObject worldContainer = Instantiate(mLevelScreenTemplate, mWorldPh);
            mLevelScreenRect = worldContainer.transform.Find("Image").GetComponent<RectTransform>();
            
            mLevelScreenRect.anchoredPosition += new Vector2((mLevelScreenRect.rect.width + mLevelScreenPad) * i, 0);

            PopuldateContainer(db.data[i], worldContainer.transform);
        }
    }

    void PopuldateContainer(WorldItem worldItem, Transform container)
    {
        //-- Iterate item data and instantiate the items
        List<LevelItem>.Enumerator iter = worldItem.levels.GetEnumerator();

        int col = 0;
        int row = 0;
        int i = 0;
        for (i = 0; iter.MoveNext(); i++)
        {
            col = i % mColSize;
            row = (int)Math.Floor((float)i / mColSize);

            CreateItemButton(iter.Current, container, col, row, i);
        }
    }

    private void ScrollToPlace()
    {
        mWorldPh.DOLocalMoveX(-(mLevelScreenRect.rect.width + mLevelScreenPad) * currentWorldIndex, mScreenScrollTime);
            //.SetEase(Ease.InBounce);
    }

    public void ScrollLeft()
    {
        currentWorldIndex = Mathf.Min( ++currentWorldIndex, mNumberOfWorlds - 1);
        ScrollToPlace();
    }

    public void ScrollRight()
    {
        currentWorldIndex = Mathf.Max(--currentWorldIndex, 0);
        ScrollToPlace();
    }

    /// <summary>
    /// Shop getter
    /// </summary>
    /// <returns></returns>
    public static WorldSelectView GetUIShop()
    {
        if (mInstance == null)
        {
            throw new Exception("Shop is not loaded");

        }

        return mInstance;


    }

    /// <summary>
    /// Creates a new Item button using the item data specified
    /// </summary>
    /// <param name="item"></param>
    /// <param name="col"></param>
    private void CreateItemButton(LevelItem item, Transform container, int col, int row, int index)
    {

        if (item == null)
        {
            Debug.Log("Cannot extract item data");
            return;
        }

        int stars = item.stars;
        int diamonds = item.diamonds;

        RectTransform ph = container.Find("item_ph").GetComponent<RectTransform>();
        RectTransform rect = container.Find("Image").GetComponent<RectTransform>();

        GameObject levelTemplate = Instantiate(mLevelTemplate, container);
        levelTemplate.SetActive(true);
        Transform levelTemplateTransform = levelTemplate.transform;
        RectTransform itemRectTransform = levelTemplateTransform.GetComponent<RectTransform>();
        RectTransform itemBGRectTransform = levelTemplateTransform.Find("Image").GetComponent<RectTransform>();

        //--Set the headline text
        levelTemplate.GetComponentInChildren<TextMeshProUGUI>()
            .SetText("Level " + (index + 1));


        float shopItemWidth = itemBGRectTransform.rect.width;
        float shopItemHeight = itemBGRectTransform.rect.height;

        //--Align the item to the correct position in screen
        itemRectTransform.anchoredPosition = 
            rect.anchoredPosition +
            new Vector2(itemBGRectTransform.rect.width / 2, - itemBGRectTransform.rect.height / 2) +
            ph.anchoredPosition +
            new Vector2(col * (shopItemWidth + mPaddingX), -row * (shopItemHeight + mPaddingY));


        //--Populate stars
        Transform starsHolder = levelTemplate.transform.Find("stars_holder");
        Rect starRect = mStarTemplate.GetComponentsInChildren<RectTransform>()[0].rect;
        
        for (int i=0;i <= 2; i++)
        {
            //--Instantiate
            GameObject star = Instantiate(mStarTemplate, starsHolder);
            //--Offset to the right position
            star.GetComponent<RectTransform>().anchoredPosition +=
                new Vector2(0, starRect.height / 2) +
                itemBGRectTransform.anchoredPosition + 
                new Vector2( i * starRect.width, 0);

            if (i <= item.stars - 1)
            {
                Transform a = star.transform.Find("image_on");
                a.gameObject.SetActive(true);
                star.transform.Find("image_off").gameObject.SetActive(false);
            }
        }
    
        //--Populate diamonds
        Transform diamondsHolder = levelTemplate.transform.Find("diamonds_holder");
        Rect diamondsRect =  mDiamondTemplate.GetComponentsInChildren<RectTransform>()[0].rect;

        //--Populate diamonds
        for (int i = 0; i <= 2; i++)
        {
            GameObject diamond = Instantiate(mDiamondTemplate, diamondsHolder);
            //--Offset to the right position
            diamond.GetComponent<RectTransform>().anchoredPosition +=
                new Vector2(0, diamondsRect.height / 2) +
                itemBGRectTransform.anchoredPosition + 
                new Vector2(i * diamondsRect.width, 0);

            if (i <= item.diamonds - 1)
            {
                diamond.transform.Find("image_on").gameObject.SetActive(true);
                diamond.transform.Find("image_off").gameObject.SetActive(false);
            }
        }

        //levelTemplate.GetComponentInChildren<Button>().onClick.AddListener(() =>
        //{
        //    Debug.Log("Start world: " + currentWorldIndex + ", level: "  + index);
        //});

    }

    #endregion

}
