using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
// Struct for Upgrades
public struct Upgrade
{
    public int level;
    public int initialCost;
    public int costIncrement;
}


[Serializable]
public struct GameStatus
{
    [Header("Player Data")]
    public string playerName;
    //public Vector3 playerPosition;
    public Transform playerPosition; // Changed to Transform for spawn point
    public int health;
    public int cogWheels;
    public int coinsCollected;

    [Header("Game Data")]
    public int currentLevel;
    public string spawnPoint;   
    public List<Vector3> NPCs;
    public float totalPlaytime; 
    public int enemiesDefeated; 

    [Header("Statistical Data")]
    public int highScore; 
    public int previousScore; 

    // Add fields for upgrades
    [Header("Upgrades")]
    public Upgrade moveSpeedUpgrade;
    public Upgrade turnSpeedUpgrade;

    // Add fields for environmental data
    [Header("Environmental Data")]
    public string environmentState;
    public string weather;
    public string timeOfDay;
    public string terrainType;
    public List<SceneObjectData> sceneObjects; // List of scene objects
}

// Create asset menu item called GameManager
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameManager_SO", order = 1)]

// Create Game Manager class that extends ScriptableObject
public class SO_GameManager : ScriptableObject
{
    // Declare Struct for GameStatus (HUD Data)
    public GameStatus gameStatus;

    // Use this for initialization
    public void Start()
    {
        //LoadGameStatus();
    }

    //this function loads a saving file if found
    public void LoadGameStatus()
    {
        // Check for previous play or death!
        if (gameStatus.playerName == null || gameStatus.health <= 0)
        {
            // If new game, create new struct
            gameStatus = new GameStatus();
            //initilise a new game status
            resetGame();
            Debug.Log("File not found");
        }
        else
        {
            // Do nothing
        }
    }

    public void resetGame()
    {
        //initilise a new game status
        gameStatus.playerName = "YoMomma";
        gameStatus.currentLevel = 1;
        gameStatus.spawnPoint = null; // Set to null initially
        gameStatus.health = 100;
        gameStatus.cogWheels = 0;
        gameStatus.coinsCollected = 0;
        gameStatus.playerPosition = null; // Set to null initially
        gameStatus.NPCs = new List<Vector3>(){  new Vector3(70, 0, 65) }; 
        gameStatus.totalPlaytime = 0f; // Initialize this field
        gameStatus.enemiesDefeated = 0; // Initialize this field
        gameStatus.environmentState = "Default"; // Initialize this field
        gameStatus.highScore = 0; // Initialize this field
        gameStatus.previousScore = 0; // Initialize this field

        // Initialize upgrades
        gameStatus.moveSpeedUpgrade = new Upgrade { level = 0, initialCost = 2, costIncrement = 1 };
        gameStatus.turnSpeedUpgrade = new Upgrade { level = 0, initialCost = 1, costIncrement = 1 };

        // Initialize scene objects
        gameStatus.sceneObjects = new List<SceneObjectData>();
    }

    //build our UI controls- a simple label
    public string UpdateStatus()
    {
        //building the formatted string to be shown to the user
        string message = "";
        message += "Player Name: " + gameStatus.playerName + "\n";
        message += "Current Level: " + gameStatus.currentLevel + "\n";
        message += "Spawn Point: " + (gameStatus.spawnPoint != null ? gameStatus.spawnPoint : "None") + "\n";
        message += "Health: " + gameStatus.health + "\n";
        message += "CogWheels: " + gameStatus.cogWheels + "\n";
        message += "Coins: " + gameStatus.coinsCollected + "\n";
        message += "Total Playtime: " + gameStatus.totalPlaytime + "\n"; // Add this line
        message += "Enemies Defeated: " + gameStatus.enemiesDefeated + "\n"; // Add this line
        message += "Environment State: " + gameStatus.environmentState + "\n"; // Add this line
        message += "High Score: " + gameStatus.highScore + "\n"; // Add this line
        message += "Previous Score: " + gameStatus.previousScore + "\n"; // Add this line
        message += "Move Speed Level: " + gameStatus.moveSpeedUpgrade.level + "\n"; // Add this line
        message += "Turn Speed Level: " + gameStatus.turnSpeedUpgrade.level + "\n"; // Add this line
        message += "Weather: " + gameStatus.weather + "\n"; // Add this line
        message += "Time of Day: " + gameStatus.timeOfDay + "\n"; // Add this line
        message += "Terrain Type: " + gameStatus.terrainType + "\n"; // Add this line
        return message;
    }
}