using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private Transform crosshair;
    private SpriteRenderer gunSprite;
    [SerializeField]
    private SpriteRenderer ownerSprite;
    private Vector3 gunPosition;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private float RoundsPerMinute = 600f;
    private float shootInput;
    private float fireCooldown;
    private float cooldownTimer;

    void Start()
    {
        fireCooldown = 60f / RoundsPerMinute;
        cooldownTimer = 0f;
        gunSprite = GetComponent<SpriteRenderer>();
        gunPosition = transform.position;
    }
    void Update()
    {
        Vector3 direction = crosshair.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        AdjustGunAngle(angle);

        if (cooldownTimer > 0f)
        {
        cooldownTimer -= Time.deltaTime;
        }
        if (shootInput > 0 && cooldownTimer <= 0f)
        {
            Shoot(direction);
            cooldownTimer = fireCooldown;
        }
    }
    public void SetShootInput(float input)
    {
        shootInput = input;
    }
    private void AdjustGunAngle(float gunAngle)
    {
        if (gunAngle > 90f || gunAngle < -90f)
        {
            transform.localRotation = Quaternion.Euler(180f, 0f, -gunAngle);
            gunSprite.sortingOrder = ownerSprite.sortingOrder - 1;
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, gunAngle);
            gunSprite.sortingOrder = ownerSprite.sortingOrder + 1;
        }
    }
    void Shoot(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetDirection(direction);
        // TODO add some shooting animation/muzzle flash, sound
    }
}