using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;


//game status data structure
[Serializable]
public struct GameStatus
{
    public string playerName;
    public int currentLevel;
    public string spawnPoint;
    public int health;
    public int cogWheels;
}

public class GameManager : MonoBehaviour
{
    CogWheelCollectable cogWheelCollectable;
    public TextMeshProUGUI cogWheelText;

    public TextMeshProUGUI text;

    GameStatus gameStatus;
    string filePath;
    const string FILE_NAME = "SaveStatus.json";

    //build our UI controls- a simple label
    void ShowStatus()
    {
        //building the formatted string to be shown to the user
        string message = "";
        message += "Player Name: " + gameStatus.playerName + "\n";
        message += "Current Level: " + gameStatus.currentLevel + "\n";
        message += "Spawn Point: " + gameStatus.spawnPoint + "\n";
        message += "Health: " + gameStatus.health + "\n";
        message += "CogWheels: " + gameStatus.cogWheels + "\n";
        //updating the UI label
        text.text = message;
    }

    //this function emulates a random game event that changes the player's statistics
    public void RandomiseGameStatus()
    {
        //the namespace was specified to avoid conflicts (System.Random vs UnityEngine.Random)
        gameStatus.cogWheels += (int)Mathf.Floor(UnityEngine.Random.Range(20.0f, 100.0f));
        //simulates a level up
        if (gameStatus.cogWheels > 100)
        {
            gameStatus.currentLevel++;
            gameStatus.health += 10;
            gameStatus.cogWheels = 0;
        }
    }

    //this function loads a saving file if found
    public void LoadGameStatus()
    {
        //always check the file exists
        if (File.Exists(filePath + "/" + FILE_NAME))
        {
            //load the file content as string
            string loadedJson = File.ReadAllText(filePath + "/" + FILE_NAME);
            //deserialise the loaded string into a GameStatus struct
            gameStatus = JsonUtility.FromJson<GameStatus>(loadedJson);
            Debug.Log("File loaded successfully");
        }
        else
        {
            //initilise a new game status
            gameStatus.playerName = "YoMomma";
            gameStatus.currentLevel = 1;
            gameStatus.spawnPoint = "Beginning";//reference to a game object
            gameStatus.health = 100;
            gameStatus.cogWheels = 0;
            Debug.Log("File not found");
        }
    }

    //this function overrides the saving file
    public void SaveGameStatus()
    {
        //serialise the GameStatus struct into a Json string
        string gameStatusJson = JsonUtility.ToJson(gameStatus);
        //write a text file containing the string value as simple text
        File.WriteAllText(filePath + "/" + FILE_NAME, gameStatusJson);
        Debug.Log("File created and saved");
    }
    // Use this for initialization
    void Start()
    {
        //retrieving saving location
        filePath = Application.dataPath;
        gameStatus = new GameStatus();
        Debug.Log(filePath);
        //startup initialisation
        LoadGameStatus();
        ShowStatus();
    }
    // Update is called once per frame
    void Update()
    {
        ShowStatus();
    }

    public void AddCogWheel(int cogWheelValue)
    {
        gameStatus.cogWheels += cogWheelValue;
        cogWheelText.text = gameStatus.cogWheels.ToString();
    }


    public void RemoveCogWheel()
    {
        gameStatus.cogWheels--;
    }


    public void ResetGameStatus()
    {
        gameStatus.playerName = "YoMamma";
        gameStatus.currentLevel = 1;
        gameStatus.spawnPoint = "Beginning";//reference to a game object
        gameStatus.health = 100;
        gameStatus.cogWheels = 0;
        SaveGameStatus();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
