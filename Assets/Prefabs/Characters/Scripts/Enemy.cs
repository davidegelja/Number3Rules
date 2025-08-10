using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CharacterController2D characterController;
    private GunController gunController;
    [SerializeField]
    private float attackInterval = 1.5f;
    [SerializeField]
    private float shootRange = 10f;
    private float attackTimer;

    [SerializeField]
    private Transform[] patrolPoints;
    [SerializeField]
    private float chaseRange = 7f;
    [SerializeField]
    private float patrolSpeed = 0.5f;
    [SerializeField]
    private float chaseSpeed = 1f;
    [SerializeField]
    private Transform target;
    private int currentPatrolIndex = 0;
    private int patrolDirection = 1;
    [SerializeField]
    private float nextPatrolPointDistance = 0.2f;
    [SerializeField]
    private float verticalDifferenceToTriggerJump = 0.4f;
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

        if (distance < chaseRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
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
    public void setTarget(Transform newTarget)
    {
        target = newTarget;
        gunController.SetCrosshair(newTarget);
    }
    private void ChasePlayer()
    {
        setTarget(target);
        characterController.SetCrosshair(target);
        characterController.SetMoveSpeed(chaseSpeed);
        float moveDir = Mathf.Sign(target.position.x - transform.position.x);
        characterController.SetMoveInput(moveDir);
    }
    private void Patrol()
    {
        if (patrolPoints.Length == 0)
        {
            characterController.SetMoveInput(0);
            return;
        }

        characterController.SetMoveSpeed(patrolSpeed);
        Transform wayPoint = patrolPoints[currentPatrolIndex];
        float direction = Mathf.Sign(wayPoint.position.x - transform.position.x);
        characterController.SetMoveInput(direction);

        if (Vector2.Distance(transform.position, wayPoint.position) < nextPatrolPointDistance)
        {
            currentPatrolIndex += patrolDirection;
            if (currentPatrolIndex >= patrolPoints.Length - 1 || currentPatrolIndex <= 0)
            {
                patrolDirection *= -1;
            }
            float verticalDiff = patrolPoints[currentPatrolIndex].position.y - transform.position.y;
            if ( verticalDiff > verticalDifferenceToTriggerJump)
            {
                characterController.Jump();
            }
        }
    }
}