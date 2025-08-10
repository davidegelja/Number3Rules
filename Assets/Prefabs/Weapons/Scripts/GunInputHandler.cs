using UnityEngine;

public class GunInputHandler : MonoBehaviour
{
    private GunController controller;

    void Start()
    {
        controller = GetComponentInChildren<GunController>();
    }

    void Update()
    {
        float shoot = Input.GetButton("Fire1") ? 1f : 0f;
        controller.SetShootInput(shoot);
    }
}