using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
public class Scene2_Script1 : MonoBehaviour
{
    int lifePoints;
    string filePath;
    const string FILE_NAME = "SaveGame.txt";
    //this function emulates a random game event that changes the player life
    public void RandomiseLifePoints()
    {
        //the namespace was specified to avoid conflicts (System.Random vs UnityEngine.Random)
        lifePoints = (int)Mathf.Floor(UnityEngine.Random.Range(0.0f, 100.0f));
    }
    //this function overrides the saving file
    public void SaveLifePoints()
    {
        File.WriteAllText(filePath + "/" + FILE_NAME, lifePoints.ToString());
        Debug.Log("File created and saved");
    }

    //this function loads a saving file if found
    public void LoadLifePoints()
    {
        //always check the file exists
        if (File.Exists(filePath + "/" + FILE_NAME))
        {
            lifePoints = Int32.Parse(File.ReadAllText(filePath + "/" + FILE_NAME));
            Debug.Log("File loaded successfully");
        }
        else
        {
            lifePoints = 0;
            Debug.Log("File not found");
        }
    }
    // Use this for initialization
    void Start()
    {
        //retrieving saving location
        filePath = Application.persistentDataPath;
        Debug.Log(filePath);
        //startup initialisation
        LoadLifePoints();
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "HP: " + lifePoints;
    }
}
