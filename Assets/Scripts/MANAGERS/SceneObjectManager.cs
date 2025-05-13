using System;
using UnityEngine;

[Serializable]
public class SceneObjectData
{
    public GameObject sceneObject;
    public string objectName;
    public Vector3 position;
}

public class SceneObjectManager : MonoBehaviour
{
    private GameManager gameManager;
    private const string SCENE_OBJECT_TAG = "SceneObject";

    void Start()
    {
        gameManager = GameManager.instance;
        LoadSceneObjects();
    }

    public void SaveSceneObjects()
    {
        gameManager.soGameManager.gameStatus.sceneObjects.Clear();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(SCENE_OBJECT_TAG))
        {
            SceneObjectData data = new SceneObjectData
            {
                sceneObject = obj,
                position = obj.transform.position
            };
            gameManager.soGameManager.gameStatus.sceneObjects.Add(data);
        }
        gameManager.SaveGameStatus();
    }

    public void LoadSceneObjects()
    {
        foreach (SceneObjectData data in gameManager.soGameManager.gameStatus.sceneObjects)
        {
            if (data.sceneObject != null)
            {
                data.sceneObject.transform.position = data.position;
            }
        }
    }
}