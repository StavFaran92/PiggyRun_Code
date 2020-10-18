/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.SaveSystem;

public class GameHandler : MonoBehaviour {

    private void Awake() {
        SaveSystem.Init();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            Load();
        }
    }

    private void Save() {
        // Save
        int highScore = 15;
        int coins = 10;
        int diamonds = 20;

        SavedDataObject saveObject = new SavedDataObject
        {
            HighScore = highScore,
            Coins = coins,
            Diamonds = diamonds

        };
        string json = JsonUtility.ToJson(saveObject);
        SaveSystem.Save(json);

        Debug.Log("Saved successfuly");
    }

    private void Load() {
        // Load
        string saveString = SaveSystem.Load();
        if (saveString != null) {
            Debug.Log("Loaded: " + saveString);

            SavedDataObject saveObject = JsonUtility.FromJson<SavedDataObject>(saveString);

            Debug.Log("highscore: "+saveObject.HighScore + ", coins: " + saveObject.Coins + ", diamonds: " + saveObject.Diamonds);
        } else {
            Debug.Log("No save");
        }
    }


    
}