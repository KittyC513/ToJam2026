using UnityEngine;
using UnityEngine.UI;

public class PulseOutline : MonoBehaviour
{

    public Outline outline;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        Color color = outline.effectColor;

        color.a = Mathf.PingPong(Time.time, 1f);

        outline.effectColor = color;
    }
}
