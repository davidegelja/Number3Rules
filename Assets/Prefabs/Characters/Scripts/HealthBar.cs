using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Transform greenBar;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = greenBar.localScale;
    }
    public void SetHealthPercent(float healthPercent)
    {
        healthPercent = Mathf.Clamp01(healthPercent);
        Vector3 scale = originalScale;
        scale.x = originalScale.x * healthPercent;
        greenBar.localScale = scale;
    }
}
