using UnityEngine;

public class MagPickup : MonoBehaviour
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
            abilityManager.magArm.SetActive(true);
            Destroy(gameObject);
        }
    }
}
