using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IOHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {


        //Debug.Log("Application in editor");

        gameObject.GetComponent<SwipeDetector>().enabled = !Application.isEditor;
        gameObject.GetComponent<IOTouchSwipeHandler>().enabled = !Application.isEditor;

        gameObject.GetComponent<IOMouseHandler>().enabled = Application.isEditor; 



            //Debug.Log("Application in device");
            //gameObject.GetComponent<SwipeDetector>().enabled = true;
            //gameObject.GetComponent<IOTouchSwipeHandler>().enabled = true ;

    }
}
