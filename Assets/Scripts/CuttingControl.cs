using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CuttingControl : MonoBehaviour
{
    public Toggle cuttingToggle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cuttingToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnToggleValueChanged(bool isOn)
    {
        EventManager.isCutting = isOn;
        print("Cutting mode: " + (isOn ? "ON" : "OFF"));
    }
}
