using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage! HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " died!");
        Enemy enemy = GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.enabled = false;
        }
        CharacterController2D controller = GetComponent<CharacterController2D>();
        if (controller != null)
        {
            controller.enabled = false;
        }
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = false;
        }
        foreach (var col in GetComponents<Collider2D>())
        {
            col.enabled = false;
        }
        Animator animator = GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.Play("Death", 0, 0f);
            // animator.SetBool("isDead", true);
        }
        Destroy(gameObject, 1f);
    }
}