using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CharacterController2D characterController;
    private GunController gunController;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float followRange = 15f;
    [SerializeField]
    private float attackInterval = 1.5f;
    [SerializeField]
    private float shootRange = 10f;
    private float attackTimer;
    void Start()
    {
        characterController = GetComponent<CharacterController2D>();
        gunController = GetComponentInChildren<GunController>();
        attackTimer = 2f;
    }
    void Update()
    {
        if (attackTimer > 0f)
        {
        attackTimer -= Time.deltaTime; 
        }

        float distance = Vector2.Distance(target.position, transform.position);

        if (distance < followRange)
        {
            float moveDir = Mathf.Sign(target.position.x - transform.position.x);
            characterController.SetMoveInput(moveDir);
        }
        else
        {
            characterController.SetMoveInput(0);
        }

        if (distance < shootRange && attackTimer <= 0f)
        {
            gunController.SetShootInput(1f);
            attackTimer = attackInterval;
        }
        else
        {
            gunController.SetShootInput(0f);
        }
    }
}