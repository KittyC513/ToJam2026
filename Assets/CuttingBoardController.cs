using System;
using UnityEngine;

public class CuttingBoardController : MonoBehaviour
{

    // Index 0 = cheese that will be cuts slot / Index 1 = the new cheese slot
    public SlotScript[] isChild = Array.Empty<SlotScript>();

    bool isChildOne = false;
    bool isChildTwo = false;

    public GameObject objectToGive;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        
    }



    public void giveDataToClone()
    {
        if (isChild[0] != null && isChild[1] == null)
        {
            float givenValue = isChild[0].GiveCheese();
            isChild[1].NewCheese(isChild[0].heldItem.gameObject, givenValue);

        }
    }

}
