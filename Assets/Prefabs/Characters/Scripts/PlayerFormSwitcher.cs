using UnityEngine;

public class PlayerFormSwitcher : MonoBehaviour
{
    public GameObject[] forms;
    private int currentFormIndex = 0;

    void Start()
    {
        ActivateForm(currentFormIndex);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchForm(false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SwitchForm(true);
        }
    }

    void SwitchForm(bool nextForm)
    {
        int nextIndex = nextForm ? 1: -1;
        if (currentFormIndex + nextIndex < 0 || currentFormIndex + nextIndex >= forms.Length)
        {
            return;
        }
        Vector3 pos = forms[currentFormIndex].transform.position;
        Quaternion rot = forms[currentFormIndex].transform.rotation;

        forms[currentFormIndex].SetActive(false);

        currentFormIndex += nextIndex;

        forms[currentFormIndex].transform.position = pos;
        forms[currentFormIndex].transform.rotation = rot;

        forms[currentFormIndex].SetActive(true);
    }

    void ActivateForm(int index)
    {
        foreach (GameObject form in forms)
        {
            form.SetActive(false);
        }

        forms[index].SetActive(true);
    }
}
