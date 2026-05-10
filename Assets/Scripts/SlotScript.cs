using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class slotOpenedEvent : UnityEvent { }

public class SlotScript : MonoBehaviour
{
    public slotOpenedEvent slotType;

    [Tooltip("Swiss = 1 // Brie = 2 // Weight1 = 50 // Weight2 = 100 // weight3 = 250 // weight4 = 500")]
    // Swiss = 1 // Brie = 2 // Weight1 = 50 // Weight2 = 100 // weight3 = 250 // weight4 = 500
    public int objectType;
    public bool ignoreType = false;
    //
    public int selfIndex;
    public bool isWheel = false;

    public ObjectMovementScript heldItem = null;
    public ObjectMovementScript wantsToBeHeld = null;
    
    public int keyPointType;
    public ShelfSlots shelfSlots;

    public CircleCollider2D selfCollider;

    public GameObject slotToClone;

    public CuttingBoardController controllerCut;

    public float toBeCut = 0;

    private List<GameObject> detectedObjects = new List<GameObject>();

    void Start()
    {
        if (this.gameObject.GetComponent<CircleCollider2D>() != null)
        {
            selfCollider = this.gameObject.GetComponent<CircleCollider2D>();
        }
        if(selfCollider != null)
        {
            detectedObjects = new List<GameObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(wantsToBeHeld != null && heldItem == null && wantsToBeHeld.submitToTaker == true)
        {
            Debug.Log("4 Alert");
            heldItem = wantsToBeHeld;
            wantsToBeHeld.GetSlotted(this);
            wantsToBeHeld.submitToTaker = false;
        }
    }

    public void  OnTriggerStay2D(Collider2D collision)
    {
        if(objectType != 25)
        {
            // Might change it so the tags are cheese, wheel, and weight
            if (heldItem == null && collision.gameObject.CompareTag("Object"))
            {
                Debug.Log("1 Alert");
                if (!detectedObjects.Contains(collision.gameObject))
                {
                    Debug.Log("2 Alert");
                    detectedObjects.Add(collision.gameObject);

                    if (collision.gameObject.GetComponent<ObjectMovementScript>() && wantsToBeHeld == null)
                    {
                        if (collision.gameObject.GetComponent<ObjectMovementScript>().objectType == this.objectType || this.ignoreType)
                        {
                            Debug.Log("3 Alert");
                            wantsToBeHeld = collision.gameObject.GetComponent<ObjectMovementScript>();
                            wantsToBeHeld.beingSlotted = true;
                        }

                    }

                }
            }
        }
        
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(wantsToBeHeld != null)
        {
            if (wantsToBeHeld.GetComponent<ObjectMovementScript>() && collision.gameObject == wantsToBeHeld.gameObject)
            {
                objectLeft(wantsToBeHeld.gameObject);
            }
        }
    }

    public void objectLeft(GameObject lostObject)
    {
        if (wantsToBeHeld != null)
        {
            if (detectedObjects.Contains(wantsToBeHeld.gameObject))
            {
                detectedObjects.Remove(lostObject);
                Debug.Log("Object left slot: " + selfIndex);
            }

            wantsToBeHeld.beingSlotted = false;
            wantsToBeHeld.submitToTaker = false;
            heldItem = null;
            wantsToBeHeld = null;
            slotType?.Invoke();
        }
    }

    // pushed by cutting board
    public void NewCheese(GameObject gameObject, float newSize)
    {
        if (objectType == 25 && slotToClone != null)
        {
            slotToClone = Instantiate(gameObject, this.transform);
            heldItem = slotToClone.GetComponent<ObjectMovementScript>();
            wantsToBeHeld = heldItem;
            heldItem.controlTaken = this;
        }
    }

    // requested by cutting board
    public float GiveCheese()
    {
        if (objectType == 24 && heldItem != null)
        {
            return toBeCut;
        }
        else
        { 
            return 1; 
        }
    }

    //Called by cut script
    public void setCutSize(float forMe,float forYou)
    {
        toBeCut = forYou;
        heldItem.currentWedgeSize = forMe;
        controllerCut.giveDataToClone();

    }
}
