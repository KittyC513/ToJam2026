using System;
using UnityEngine;
using TMPro;

public class DigitalScale : MonoBehaviour
{

    public SlotScript isChild;

    public TMP_Text scaleReadOff;

    private bool nullChild = true;

    private bool isEmpty = true;

    private float currentWeight = 0;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isChild != null)
        {
            nullChild = false;
            isChild.ignoreType = true;
            Debug.Log("Childfound");
        }
        displayCurrentWeight();
    }

    // Update is called once per frame
    void Update()
    {
        if (!nullChild)
        {
            if (isEmpty && isChild.heldItem != null)
            {
                checkCheeseWeight();
                isEmpty = false;
            }
            else if (!isEmpty && isChild.heldItem == null)
            {
                checkCheeseWeight();
                isEmpty = true;
            }
        }



    }

    private void checkCheeseWeight()
    {
        if (!nullChild)
        {
            Debug.Log("WeightCheckStart");

            if (isChild.heldItem != null)
            {
                // change to actual script
                if (isChild.heldItem.GetComponent<TempCheese>())
                {
                    currentWeight = isChild.heldItem.GetComponent<TempCheese>().tempCheeseWeight;
                    displayCurrentWeight();
                }
            }
            else
            {
                currentWeight = 0;
                displayCurrentWeight();
            }

        }
    }

    private void displayCurrentWeight()
    {
        if(scaleReadOff != null)
        {
            scaleReadOff.text = (currentWeight + "g");
            Debug.Log("WeightCheckFinish: " + currentWeight + "g");
        }
    }

}
