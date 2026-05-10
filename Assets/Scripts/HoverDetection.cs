using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HoverDetection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool hovering;
    Outline outline;
    public RectTransform rectTransform;
    public Image radialBar;

    public CuttingFeature cuttingFeature;

    public float radius;



    //Dectect when the mouse is hovering over the UI element

    private void Awake()
    {

        outline = GetComponent<Outline>();
        outline.enabled = false;

    }

    private void Start()
    {
        radialBar.alphaHitTestMinimumThreshold = 0.1f;
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
        //outline.enabled = true;
        //Debug.Log("Mouse entered UI");
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        //outline.enabled = false;
        //Debug.Log("Mouse exited UI");
    }

    private void Update()
    {
        RadialHoverDetect();
    }
    void RadialHoverDetect()
    {
        Vector2 mousePos = Input.mousePosition;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, mousePos, null, out Vector2 localPoint);

        //radius check
        radius = cuttingFeature.radius;
        bool insideCircle = localPoint.magnitude <= radius;

        //angle check
        float angle = Mathf.Atan2(localPoint.y, localPoint.x) * Mathf.Rad2Deg;

        if(angle < 0)
        {
            angle += 360;
        }

        float startAngle = cuttingFeature.staticLine.eulerAngles.z;

        float sectorSize = cuttingFeature.cutAmount * 360f;

        float delta = (angle - startAngle + 360) % 360f;

        bool insideSector = delta <= sectorSize;

        bool validHover = insideCircle && insideSector;

        if (validHover)
        {
            outline.enabled = true;
            Debug.Log("Mouse is hovering over the valid area");
        }
        else
        {
            outline.enabled = false;
            Debug.Log("Mouse is NOT hovering over the valid area");
        }
    }

}
