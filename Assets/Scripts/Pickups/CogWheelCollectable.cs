using UnityEngine;
using TMPro;

public class CogWheelCollectable : MonoBehaviour
{
    GameManager gameManager;
    public int cogWheelValue = 1;


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.AddCogWheel(cogWheelValue);
            Destroy(gameObject);
        }
    }

}
