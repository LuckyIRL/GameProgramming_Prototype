using UnityEngine;

public class GrapplePickup : MonoBehaviour
{
    AbilityManager abilityManager;

    private void Start()
    {
        abilityManager = GameObject.Find("AbilityManager").GetComponent<AbilityManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            abilityManager.grappleArm.SetActive(true);
            Destroy(gameObject);
        }
    }
}
