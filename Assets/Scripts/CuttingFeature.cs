using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CuttingFeature : MonoBehaviour, IDragHandler
{
    public bool isCutting;
    public RectTransform line;
    public float cutSpeed = 10f;
    public Image radialBar;
    public RectTransform staticLine;
    public RectTransform cuttingLine;
    public Color radialBarColor;

    private void Start()
    {
        radialBarColor = radialBar.color;
        radialBarColor.a = 0.25f;
        radialBar.color = radialBarColor;

    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        isCutting = true;
        // Calculate the direction from the line to the mouse position
        Vector2 direction = eventData.position - (Vector2)RectTransformUtility.WorldToScreenPoint(null, line.position);

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Adjust the angle to account for the initial orientation of the line
        angle -= 90f;

        //convert negative angles
        if(angle< 0)
        {
            angle += 360f;
        }

        // Smoothly rotate the line towards the target angle
        Quaternion target = Quaternion.Euler(0, 0, angle);

        line.rotation = Quaternion.Lerp(line.rotation, target, Time.deltaTime * cutSpeed);

    }

     void Update()
    {
        if(isCutting) 
            Cut();
    }

    void Cut()
    {
        // Calculate the angle difference between the static line and the cutting line
        float staticAngle = staticLine.eulerAngles.z;
        float cuttingAngle = cuttingLine.eulerAngles.z - 180f;

        float angleDifference = (staticAngle - cuttingAngle + 360f) % 360f;

        radialBar.fillAmount = 1 - (angleDifference / 360);

        radialBarColor.a = 0.25f;
        radialBar.color = radialBarColor;


        // If the mouse left button is released, consider the cut is done
        if (Input.GetMouseButtonUp(0))
        {
            radialBarColor.a = 0.75f;
            radialBar.color = radialBarColor;
            isCutting = false;
        }
            
    }


}
