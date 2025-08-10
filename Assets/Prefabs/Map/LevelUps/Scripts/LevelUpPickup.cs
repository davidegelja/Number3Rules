using UnityEngine;
using System.Collections;

public class LevelUpPickup : MonoBehaviour
{
    public float launchForce = 5f;
    public float upwardForce = 2f;
    public float spinForce = 300f;
    public float lifetime = 3f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.enabled = false;
        PlayerFormSwitcher switcher = other.GetComponentInParent<PlayerFormSwitcher>();
        if (switcher != null)
        {
            switcher.LevelUp();
            StartCoroutine(LaunchIntoBackground());
        }
    }
    private IEnumerator LaunchIntoBackground()
    {
        Animator animator = GetComponent<Animator>();
        animator.enabled = false;
        Destroy(GetComponent<BoxCollider2D>());
        yield return null;

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        if (GetComponent<BoxCollider>() == null)
            gameObject.AddComponent<BoxCollider>();

        rb.useGravity = true;
        rb.isKinematic = false;

        Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), upwardForce, Random.Range(2f, 4f)).normalized;
        Debug.Log("Applying force: " + randomDir * launchForce);
        rb.AddForce(randomDir * launchForce, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * spinForce, ForceMode.Impulse);

        Destroy(gameObject, lifetime);
    }
}
