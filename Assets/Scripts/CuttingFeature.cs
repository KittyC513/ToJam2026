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
    public Image cheessBar;
    public Image fillBar;
    public RectTransform staticLine;
    public RectTransform cuttingLine;
    private Transform originalPos;
    public Color radialBarColor;

    public RectTransform fill;

    //public RectTransform fill;
    public float cutAmount = 1;

    public float cheeseSize = 1;

    private void Start()
    {
        originalPos = cuttingLine.transform;
        radialBar = fillBar;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        //Initial setup for the cutting process, enable the fillBar and set its color to a semi-transparent version of its original color
        if (!fillBar.enabled)
            fillBar.enabled = true;


        radialBar.fillAmount = cutAmount;
        radialBar = fillBar;
        radialBarColor = radialBar.color;
        radialBarColor.a = 0.25f;
        radialBar.color = radialBarColor;
        fill = radialBar.rectTransform;


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
        if(isCutting && radialBar != null) 
            Cut();
    }

    void Cut()
    {

        // Calculate the angle difference between the static line and the cutting line
        float staticAngle = staticLine.eulerAngles.z;
        float cuttingAngle = cuttingLine.eulerAngles.z - 180f;

        float angleDifference = (staticAngle - cuttingAngle + 360f) % 360f;

        if(angleDifference <= 10)
        {
            Reset();
            return;
        }

        radialBar = fillBar;
        //radialBar.fillAmount = 1 - (angleDifference / 360);
        radialBar.fillAmount = angleDifference / 360;
        cutAmount = radialBar.fillAmount;
        // If the mouse left button is released, consider the cut is done
        if (Input.GetMouseButtonUp(0))
        {
            CheeseCutCheck();
            isCutting = false;
        }            
    }

    void Reset()
    {
        if(cutAmount >= 0.9f)
        {
            line.rotation = Quaternion.Lerp(line.rotation, originalPos.rotation, Time.deltaTime * cutSpeed);
            radialBar.fillAmount = cutAmount;
        }
    }

    void CheeseCutCheck()
    {
        if (cutAmount >= cheeseSize)
        {
            Reset();
        }
        else
        {
            radialBar = cheessBar;
            fillBar.enabled = false;

            radialBar.fillAmount = radialBar.fillAmount;
            fill = radialBar.rectTransform;

            radialBar.fillAmount = cutAmount;
            cheeseSize -= cutAmount;
        }
    }
    

}
