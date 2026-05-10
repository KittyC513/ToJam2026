using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class ShelfSlots : MonoBehaviour
{
    public slotOpenedEvent mySlotEvent;


    public SlotScript[] isChild =  Array.Empty<SlotScript>();


    public SlotScript firstFree = null;
    public SlotScript secondFree = null;


    private void OnEnable()
    {
        mySlotEvent.AddListener(CheckForEmpty);
    }

    private void OnDisable()
    {
        mySlotEvent.RemoveListener(CheckForEmpty);
    }

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
        firstFree = null;
        secondFree = null;
        Debug.Log("Empty Check Running");
        for (int i = 0; i < isChild.Length; i++)
        {
            isChild[i].selfIndex = i;
            isChild[i].shelfSlots = this;
            if (isChild[i].objectType == 1 && firstFree == null)
            {
                FindFirstEmpty(isChild[i]);
            }
            else if (isChild[i].objectType == 2 && secondFree == null)
            {
                FindFirstEmpty(isChild[i]);
            }

        }

    }

    void FindFirstEmpty(SlotScript availableSlot)
    {
        Debug.Log(availableSlot + " Recieved");
        if (availableSlot.objectType == 1)
        {
            firstFree = availableSlot;
            availableSlot.isWheel = true;
//            Debug.Log("Swiss Slot: " + availableSlot.selfIndex + " is free!");
        }
        else if (availableSlot.objectType == 2)
        {
            secondFree = availableSlot;
            availableSlot.isWheel = true;
 //           Debug.Log("Brie Slot: " + availableSlot.selfIndex + " is free!");
        }
        else
        {
  //          Debug.Log("No free cheese slot of type: " + availableSlot.cheeseType);
        }

        }


}
