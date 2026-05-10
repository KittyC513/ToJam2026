using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HoverDetection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool hovering;
    Outline outline;
    public CuttingFeature cuttingFeature;
    //Dectect when the mouse is hovering over the UI element

    private void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;

    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
        //outline.enabled = true;
        Debug.Log("Mouse entered UI");
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        //outline.enabled = false;
        Debug.Log("Mouse exited UI");
    }

}
