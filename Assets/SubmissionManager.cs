using UnityEngine;

public class SubmissionManager : MonoBehaviour
{

    public SlotScript isChild;
    public CustomerRequestController controller;

    private float cheeseWeight;
    private float cheeseSize;

    private bool readySubmit = true;

    private bool wasEntered = false;

    // Update is called once per frame
    void Update()
    {
        if (isChild != null && !wasEntered)
        {
            if (isChild.heldItem != null)
            {
                if (isChild.heldItem.objectType == controller.currentRequest.x) 
                { 
                wasEntered = true;
                    CheckForMatch(true);
                }
                else if (isChild.heldItem.objectType == controller.currentRequest.x)
                {
                    CheckForMatch(false);
                }



            }
        }
        else if (isChild != null && wasEntered)
        {
            if(isChild.heldItem == null)
            {
                wasEntered = false;
            }
        }
    }

    private void CheckForMatch(bool type)
    {
        if (isChild != null)
        {
            if (isChild.heldItem != null && readySubmit == true)
            {
                cheeseSize = isChild.heldItem.currentWedgeSize;
                cheeseWeight = isChild.heldItem.transform.parent.GetComponent<TempCheese>().tempCheeseWeight;
                float realWeight = Mathf.Round(cheeseWeight * cheeseSize);
                if(realWeight < controller.currentRequest.y + CustomerRequestController.leeway && realWeight > controller.currentRequest.y - CustomerRequestController.leeway && isChild.heldItem.objectType == controller.currentRequest.x)
                    {
                    controller.OrderResult(1);
                }
                else
                {
                    controller.OrderResult(2);

                }
                TakeProduct();
                controller.RefreashWheelCheck();
                wasEntered = false;
            }
        }
    }

    private void TakeProduct()
    {
        DestroyImmediate(this.isChild.heldItem.gameObject);
        this.isChild.ResetSlot();
    }


}
