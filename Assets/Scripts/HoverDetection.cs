using UnityEngine;
using UnityEngine.EventSystems; 

public class HoverDetection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public bool hovering;
    //Dectect when the mouse is hovering over the UI element
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
        Debug.Log("Mouse entered UI");
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        Debug.Log("Mouse exited UI");
    }

}
