using System;
using UnityEngine;

public class CuttingBoardController : MonoBehaviour
{

    // Index 0 = cheese that will be cuts slot / Index 1 = the new cheese slot
    public SlotScript[] isChild = Array.Empty<SlotScript>();

    bool isChildOne = false;
    bool isChildTwo = false;

    public bool originalRemoved = true;

    public GameObject objectToGive;

    public GameObject cuttingCheese;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (cuttingCheese != null)
        {
            if (cuttingCheese.activeSelf)
            {
                cuttingCheese.SetActive(false);
            }
        }
        for (int i = 0; i < isChild.Length; i++)
        {
            if (isChild[i] != null && i == 0)
            {
                isChild[i].selfIndex = i;
                isChild[i].objectType = 24;
                isChildOne = true;
                Debug.Log("cSlot 1 found");
            }
            else if (isChild[i] != null && i == 1)
            {
                isChildTwo = true;
                isChild[i].objectType = 25;
                isChild[i].selfIndex = i;
                Debug.Log("cSlot 2 found");
            }
        }


    }



    // Update is called once per frame
    void Update()
    {
        if (isChild[0] != null)
        {
            if (isChild[0].hasCloned == false && isChild[0].heldItem != false && originalRemoved != false)
            {
                enableBoard();
            }
        }
    }

    public void enableBoard()
    {
        cuttingCheese.SetActive(true);
        originalRemoved = false;
    }

    public void giveDataToClone()
    {
        if (isChild[0] != null && isChild[1].heldItem == null)
        {
            Debug.Log("Children exist");
            float givenValue = isChild[0].GiveCheese();
            isChild[1].NewCheese(isChild[0].heldItem.gameObject, givenValue);
            isChild[0].hasCloned = true;
        }

        if (cuttingCheese != null)
        {
                cuttingCheese.SetActive(false);
        }
    }

}
