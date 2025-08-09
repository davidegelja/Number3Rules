using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Transform crosshair;
    private SpriteRenderer gunSprite;
    [SerializeField]
    private SpriteRenderer playerSprite;
    private Vector3 gunPosition;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform firePoint;

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

        AdjustGunAngle(angle);

        if (Input.GetMouseButtonDown(0))
        {
            Shoot(direction);
        }
    }

    public void AdjustGunAngle(float gunAngle)
    {
        if (gunAngle > 90f || gunAngle < -90f)
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
        void Shoot(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }
}