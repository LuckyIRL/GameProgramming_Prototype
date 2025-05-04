using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    CogWheelCollectable cogWheelCollectable;
    public TextMeshProUGUI cogWheelText;
    public TextMeshProUGUI text;
    public TextMeshProUGUI playtimeText; // Add this field for the playtime text

    // Add references to the UI buttons
    public Button moveSpeedButton;
    public Button turnSpeedButton;

    // Reference to the SO_GameManager Scriptable Object
    public SO_GameManager soGameManager;

    string filePath;
    const string FILE_NAME = "SaveStatus.json";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make the GameManager persistent across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    //build our UI controls- a simple label
    void ShowStatus()
    {
        //building the formatted string to be shown to the user
        string message = soGameManager.UpdateStatus();
        //updating the UI label
        text.text = message;
    }

    //this function emulates a random game event that changes the player's statistics
    public void RandomiseGameStatus()
    {
        //the namespace was specified to avoid conflicts (System.Random vs UnityEngine.Random)
        soGameManager.gameStatus.cogWheels += (int)Mathf.Floor(UnityEngine.Random.Range(20.0f, 100.0f));
        //simulates a level up
        if (soGameManager.gameStatus.cogWheels > 100)
        {
            soGameManager.gameStatus.currentLevel++;
            soGameManager.gameStatus.health += 10;
            soGameManager.gameStatus.cogWheels = 0;
        }
        UpdateCogWheelText();
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
            soGameManager.gameStatus = JsonUtility.FromJson<GameStatus>(loadedJson);
            //Debug.Log("File loaded successfully: " + loadedJson);
        }
        else
        {
            //initilise a new game status
            soGameManager.resetGame();
            Debug.Log("File not found, initializing new game status");
        }
        UpdateCogWheelText();
        UpdatePlayerPosition();
    }

    //this function overrides the saving file
    public void SaveGameStatus()
    {
        //Debug.Log("Saving game status...");
        //serialise the GameStatus struct into a Json string
        string gameStatusJson = JsonUtility.ToJson(soGameManager.gameStatus);
        //write a text file containing the string value as simple text
        File.WriteAllText(filePath + "/" + FILE_NAME, gameStatusJson);
        //Debug.Log("File created and saved: " + gameStatusJson);
    }

    // Use this for initialization
    void Start()
    {
        //retrieving saving location
        filePath = Application.dataPath;
        //Debug.Log(filePath);
        //startup initialisation
        LoadGameStatus();
        ShowStatus();
    }

    // Update is called once per frame
    void Update()
    {
        // Increment the total playtime
        soGameManager.gameStatus.totalPlaytime += Time.deltaTime;

        // Update the playtime text
        UpdatePlaytimeText();
    }

    public void AddCogWheel(int cogWheelValue)
    {
        soGameManager.gameStatus.cogWheels += cogWheelValue;
        UpdateCogWheelText();
    }

    public void RemoveCogWheel()
    {
        soGameManager.gameStatus.cogWheels--;
        UpdateCogWheelText();
    }

    public void ResetGameStatus()
    {
        soGameManager.resetGame();
        SaveGameStatus();
        UpdateCogWheelText();
    }

    void OnApplicationQuit()
    {
        SaveGameStatus();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveGameStatus();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void UpdateCogWheelText()
    {
        cogWheelText.text = soGameManager.gameStatus.cogWheels.ToString();
    }

    private void UpdatePlaytimeText()
    {
        // Format the playtime as hours:minutes:seconds
        int hours = Mathf.FloorToInt(soGameManager.gameStatus.totalPlaytime / 3600);
        int minutes = Mathf.FloorToInt((soGameManager.gameStatus.totalPlaytime % 3600) / 60);
        int seconds = Mathf.FloorToInt(soGameManager.gameStatus.totalPlaytime % 60);
        playtimeText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }

    private void UpdatePlayerPosition()
    {
        // Find the player object and set its position from the saved game status
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = soGameManager.gameStatus.playerPosition;
        }
    }

    // Method to purchase move speed upgrade
    public void PurchaseMoveSpeedUpgrade()
    {
        int cost = soGameManager.gameStatus.moveSpeedUpgrade.initialCost + (soGameManager.gameStatus.moveSpeedUpgrade.level * soGameManager.gameStatus.moveSpeedUpgrade.costIncrement);
        //Debug.Log("Attempting to purchase move speed upgrade. Cost: " + cost + ", Current CogWheels: " + soGameManager.gameStatus.cogWheels);
        //Debug.Log("Move Speed Upgrade - Initial Cost: " + soGameManager.gameStatus.moveSpeedUpgrade.initialCost + ", Cost Increment: " + soGameManager.gameStatus.moveSpeedUpgrade.costIncrement);
        if (soGameManager.gameStatus.cogWheels >= cost)
        {
            soGameManager.gameStatus.cogWheels -= cost;
            soGameManager.gameStatus.moveSpeedUpgrade.level++;
            Debug.Log("Move speed upgrade purchased. New CogWheels: " + soGameManager.gameStatus.cogWheels);
            UpdateCogWheelText();
            SaveGameStatus();
        }
        else
        {
            Debug.Log("Not enough CogWheels to purchase move speed upgrade.");
        }
    }

    // Method to purchase turn speed upgrade
    public void PurchaseTurnSpeedUpgrade()
    {
        int cost = soGameManager.gameStatus.turnSpeedUpgrade.initialCost + (soGameManager.gameStatus.turnSpeedUpgrade.level * soGameManager.gameStatus.turnSpeedUpgrade.costIncrement);
        //Debug.Log("Attempting to purchase turn speed upgrade. Cost: " + cost + ", Current CogWheels: " + soGameManager.gameStatus.cogWheels);
        //Debug.Log("Turn Speed Upgrade - Initial Cost: " + soGameManager.gameStatus.turnSpeedUpgrade.initialCost + ", Cost Increment: " + soGameManager.gameStatus.turnSpeedUpgrade.costIncrement);
        if (soGameManager.gameStatus.cogWheels >= cost)
        {
            soGameManager.gameStatus.cogWheels -= cost;
            soGameManager.gameStatus.turnSpeedUpgrade.level++;
            Debug.Log("Turn speed upgrade purchased. New CogWheels: " + soGameManager.gameStatus.cogWheels);
            UpdateCogWheelText();
            SaveGameStatus();
        }
        else
        {
            Debug.Log("Not enough CogWheels to purchase turn speed upgrade.");
        }
    }
}