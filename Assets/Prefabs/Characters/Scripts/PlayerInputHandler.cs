using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private CharacterController2D controller;

    void Start()
    {
        controller = GetComponent<CharacterController2D>();
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");
        controller.SetMoveInput(move);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            controller.Jump();
    }
}
