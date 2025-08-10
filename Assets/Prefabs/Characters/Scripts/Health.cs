using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100f;
    private float currentHealth;
    private HealthBar healthBar;
    private GameObject gun;
    private GameObject healthBarObject;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<HealthBar>();
        gun = GetComponentInChildren<GunController>()?.gameObject;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (healthBar != null)
        {
            healthBar.SetHealthPercent(currentHealth / maxHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(false);
        }
        if (gun != null)
        {
            gun.SetActive(false);
        }
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