using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchAimCam : MonoBehaviour
{
    public GameObject aimCam;
    public GameObject playerCam;
    RobotInputActions robotInputActions;

    private void Start()
    {
        aimCam.SetActive(false);
        playerCam.SetActive(true);
    }

    private void Update()
    {
        if (robotInputActions.isAiming)
        {
            aimCam.SetActive(true);
            playerCam.SetActive(false);
        }
        else
        {
            aimCam.SetActive(false);
            playerCam.SetActive(true);
        }
    }
}
