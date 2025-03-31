using UnityEditor.PackageManager.UI;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Scene1_Script1 : MonoBehaviour
{
    //reference of the the UI Text object
    Text gameStatusUI;
    //declare our state variables as simple types for now
    string playerName;
    int currentLevel;
    string spawnPoint;
    int health;
    int coins;
    //now setup a getter and setter function for our state data
    void GetState()
    {
        gameStatusUI.text = "Now loading...";
        //include a check to ensure that PlayerPrefs isn't empty (first run)
        if (PlayerPrefs.HasKey("playerName"))
        {
            //get the PlayerPrefs data
            playerName = PlayerPrefs.GetString("playerName");
            currentLevel = PlayerPrefs.GetInt("currentLevel");
            spawnPoint = PlayerPrefs.GetString("spawnPoint");
            health = PlayerPrefs.GetInt("health");
            coins = PlayerPrefs.GetInt("coins");
        }
    }

    //setup the system events that will handle our file I/O
    void Awake()
    {
        //retrieve a reference to the Text object present in the scene
        gameStatusUI = GetComponent<Text>();
        //awake/resume will be getters
        GetState();
        Debug.Log("awake");
    }
    void OnApplicationPause(bool pauseStatus)
    {
        //setup a condition for pause/resume
        //we will use the editor flags to simulate pause/resume events
        if (pauseStatus)
        {
            //game is pausing, setter
            SetState();
            Debug.Log("pause");
        }
        else
        {
            //game is resuming, getter
            GetState();
            Debug.Log("resume");
        }
    }

    void SetState()
    {
        //set the PlayerPrefs data
        PlayerPrefs.SetString("playerName", playerName);
        PlayerPrefs.SetInt("currentLevel", currentLevel);
        PlayerPrefs.SetString("spawnPoint", spawnPoint);
        PlayerPrefs.SetInt("health", health);
        PlayerPrefs.SetInt("coins", coins);
        //now we need to save the data to PlayerPrefs on disk
        PlayerPrefs.Save();
    }
    //build our UI controls- a simple label
    void ShowStatus()
    {
        //building the formatted string to be shown to the user
        string message = "";
        message += "Player Name: " + playerName + "\n";
        message += "Current Level: " + currentLevel + "\n";
        message += "Spawn Point: " + spawnPoint + "\n";
        message += "Health: " + health + "\n";
        message += "Coins: " + coins + "\n";
        gameStatusUI.text = message;
    }

    //now a quit function, setter
    void OnApplicationQuit()
    {
        SetState();
        Debug.Log("quit");
    }
    // Use this for initialization
    void Start()
    {
        if (!PlayerPrefs.HasKey("playerName"))
        {
            //declare our state variables as simple types for now
            playerName = "Keith";
            currentLevel = 1;
            spawnPoint = "beginning";//reference to a game object
            health = 100;
            coins = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        coins += 1;
        //simulates a level up
        if (coins > 100)
        {
            currentLevel++;
            health += 10;
            coins = 0;
        }
        ShowStatus();
    }

}
