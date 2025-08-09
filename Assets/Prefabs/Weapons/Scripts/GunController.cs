using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Transform crosshair;
    private SpriteRenderer gunSprite;
    [SerializeField]
    private SpriteRenderer playerSprite;
    private Vector3 gunPosition;

    void Start()
    {
        gunSprite = GetComponent<SpriteRenderer>();
        gunPosition = transform.position;
    }
    void Update()
    {
        Vector3 direction = crosshair.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (angle > 90f || angle < -90f)
        {
            gunSprite.flipY = true;
            gunSprite.sortingOrder = playerSprite.sortingOrder - 1;
        }
        else
        {
            gunSprite.flipY = false;
            gunSprite.sortingOrder = playerSprite.sortingOrder + 1;
        }
    }
}