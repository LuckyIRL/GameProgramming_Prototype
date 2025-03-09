using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    CogWheelCollectable cogWheelCollectable;
    public TextMeshProUGUI cogWheelText;

    public int cogWheelCount = 0;

    void Start()
    {
       
    }

    public void AddCogWheel(int cogWheelValue)
    {
        cogWheelCount += cogWheelValue;
    }

    public void RemoveCogWheel(int cogWheelValue)
    {
        cogWheelCount -= cogWheelValue;
    }

    public void SetCogWheelCount(int newCogWheelCount)
    {
        cogWheelCount = newCogWheelCount;
    }

    void Update()
    {
        cogWheelText.text = " " + cogWheelCount;
    }
}
