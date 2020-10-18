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
using System.Text;

public static class SaveSystem {

    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
    private const string SAVE_EXTENSION = "binary";

    public static void Init() {
        // Test if Save Folder exists
        if (!Directory.Exists(SAVE_FOLDER)) {
            // Create Save Folder
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save(string data) {
        //--encrypt to bytes
         //byte[] bytes =  System.Text.Encoding.UTF8.GetBytes(data);

        //string saveString = Encoding.UTF8.GetString(bytes);
        File.WriteAllText(SAVE_FOLDER + "save" + "." + SAVE_EXTENSION, data);
        //string data = "";

        //foreach(byte b in bytes)
        //{
        //    data += b.ToString();
        //    data += " ";
        //}
        // saveNumber is unique
        //File.WriteAllText(SAVE_FOLDER + "save" + "." + SAVE_EXTENSION, data);
    }

    public static string Load() {
        DirectoryInfo directoryInfo = new DirectoryInfo(SAVE_FOLDER);
        // Get all save files
        FileInfo[] saveFiles = directoryInfo.GetFiles("save." + SAVE_EXTENSION);
        // Cycle through all save files and identify the most recent one
        FileInfo mostRecentFile = saveFiles[0];

        // If theres a save file, load it, if not return null
        if (mostRecentFile != null) {
            string saveString = File.ReadAllText(mostRecentFile.FullName);
            //byte[] bytes = Encoding.UTF8.GetBytes(TextInBytes);
            //string saveString = Encoding.UTF8.GetString(bytes);
            //string[] aa = TextInBytes.Split(' ');
            //byte[] bytes = new byte[aa.Length];
            //for(int i=0; i<aa.Length; i++)
            //{
            //    bytes[i] = new (aa[i]);
            //}
            
            //string saveString = System.Text.Encoding.UTF8.GetString(bytes); 
            return saveString;
        } else {
            return null;
        }
    }

}
