using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CharacterController2D controller;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float followRange = 15f;
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }
    void Update()
    {
        float distance = target.position.x - transform.position.x;

        if (Mathf.Abs(distance) < followRange)
        {
            float moveDir = Mathf.Sign(distance);
            controller.SetMoveInput(moveDir);
        }
        else
        {
            controller.SetMoveInput(0);
        }
    }
}