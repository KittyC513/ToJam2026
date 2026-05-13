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
                if (isChild.heldItem.objectType == 1 && controller.requestedCheese == true) 
                { 
                wasEntered = true;
                    CheckForMatch(true);
                }
                else if (isChild.heldItem.objectType == 2 && controller.requestedCheese == false)
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
             //   if(realWeight > CustomerRequestController.currentRequest.y + CustomerRequestController.leeway && realWeight < CustomerRequestController.currentRequest.y - CustomerRequestController.leeway)
                if ( realWeight != -1 && type && controller.requestedCheese)
                    {
                    controller.OrderResult(1);
                }
                else if (realWeight != -1 && !type && !controller.requestedCheese)
                {
                    controller.OrderResult(2);
                }
                TakeProduct();
                wasEntered = false;
            }
        }
    }

    private void TakeProduct()
    {
        this.isChild.ResetSlot();
    }


}
