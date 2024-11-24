using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class GrappleShooting : MonoBehaviour
{
    public GameObject grappleHook;
    public Transform FirePoint;
    public float grappleSpeed = 10.0f;
    [SerializeField] private float chainLength = 10.0f;
    private bool isGrappling = false;
    public LayerMask grappleLayer;
    public LineRenderer lineRenderer;
    CameraSwitcher cameraSwitcher;

    private void Start()
    {
        lineRenderer.positionCount = 2;
        cameraSwitcher = GetComponent<CameraSwitcher>();
    }

    private void Update()
    {
        if (isGrappling)
        {
            lineRenderer.SetPosition(0, FirePoint.position);
            lineRenderer.SetPosition(1, grappleHook.transform.position);
        }
    }

    public void Grapple()
    {
        if (isGrappling)
        {
            isGrappling = false;
            lineRenderer.enabled = false;
            cameraSwitcher.SwitchCamera();
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(FirePoint.position, FirePoint.forward, out hit, chainLength, grappleLayer))
            {
                isGrappling = true;
                lineRenderer.enabled = true;
                grappleHook = Instantiate(grappleHook, hit.point, Quaternion.identity);
                grappleHook.GetComponent<Rigidbody>().linearVelocity = FirePoint.forward * grappleSpeed;
            }
        }
    }

    public void OnGrapple(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Grapple();
        }
    }
}
