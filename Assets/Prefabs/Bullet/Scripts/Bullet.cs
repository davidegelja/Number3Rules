using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private int damage = 20;
    [SerializeField]
    private float lifetime = 2f;
    private Vector2 direction;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Health targetHealth = other.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}