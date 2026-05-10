using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ShelfSlots : MonoBehaviour
{
    public SlotScript[] isChild =  Array.Empty<SlotScript>();
    public UnityEvent<int> slotOpened;

    public SlotScript firstFree = null;
    public SlotScript secondFree = null;

    // 
    void Start()
    {
        // Determines which slot is the first empty of that cheese
        CheckForEmpty();

    }

    // 
    void Update()
    {
        
    }
    void CheckForEmpty()
    {
        for (int i = 0; i < isChild.Length; i++)
        {
            isChild[i].selfIndex = i;
            isChild[i].ShelfSlots = this;
            if (isChild[i].cheeseType == 1 && firstFree == null)
            {
                FindFirstEmpty(isChild[i]);
            }
            else if (isChild[i].cheeseType == 2 && secondFree == null)
            {
                FindFirstEmpty(isChild[i]);
            }
            Debug.Log(isChild[i].selfIndex);

        }

    }

    void FindFirstEmpty(SlotScript availableSlot)
    {
        Debug.Log(availableSlot + " Recieved");
        if (availableSlot.cheeseType == 1)
        {
            firstFree = availableSlot;
            availableSlot.isWheel = true;
            Debug.Log("Swiss Slot: " + availableSlot.selfIndex + " is free!");
        }
        else if (availableSlot.cheeseType == 2)
        {
            secondFree = availableSlot;
            availableSlot.isWheel = true;
            Debug.Log("Brie Slot: " + availableSlot.selfIndex + " is free!");
        }
        else
        {
            Debug.Log("No free cheese slot of type: " + availableSlot.cheeseType);
        }

        }


}
