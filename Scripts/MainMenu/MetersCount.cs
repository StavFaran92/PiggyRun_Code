using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MetersCount : MonoBehaviour
{
    float m_meters;

    // Start is called before the first frame update
    void Start()
    {

        GetComponentInParent<HUD>().setMetersCount(this);

        ResetMeters();
    }

    void ResetMeters()
    {
        m_meters = 0;
        Draw();
    }

    public void IncreaseMetersByAmount(float amount)
    {
        m_meters += amount;
        Draw();
    }

    ////This will probably wont be used...
    //public void DecreaseCoinsByAmount(int amount)
    //{
    //    m_meters = Mathf.Max(0, m_meters - amount);
    //    Draw();
    //}

    private void Draw()
    {

    }


}
