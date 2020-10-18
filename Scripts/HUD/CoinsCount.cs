using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsCount : MonoBehaviour
{
    TextMeshProUGUI m_CoinsView;
    int m_Coins;

    // Start is called before the first frame update
    void Start()
    {

        GetComponentInParent<HUD>().SetCoins(this);

        m_CoinsView = GetComponent<TextMeshProUGUI>();

        ResetCoins();
    }

    void ResetCoins()
    {
        m_Coins = 0;
        Draw();
    }

    public void IncreaseCoinsByAmount(int amount)
    {
        m_Coins += amount;
        Draw();
    }

    public int GetCoins()
    {
        return m_Coins;
    }

    //This will probably wont be used...
    public void DecreaseCoinsByAmount(int amount)
    {
        m_Coins = Mathf.Max(0, m_Coins - amount);
        Draw();
    }

    private void Draw()
    {
        m_CoinsView.SetText(m_Coins.ToString());
    }


}
