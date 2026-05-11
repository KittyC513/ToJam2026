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

    public bool hasCloned = false;

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

                if (hasCloned == true && this.objectType == 24)
                {
                    hasCloned = false;
                    controllerCut.originalRemoved = true;
                }
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

    public void cutTheCheese()
    {
        if (this.objectType == 24 && hasCloned == false)
        {
            hasCloned = true;
        }
    }

    // pushed by cutting board
    public void NewCheese(GameObject gameObject, float newSize)
    {
        Debug.Log("Clone Logic Initiated");
        if (objectType == 25 && slotToClone == null)
        {

            slotToClone = Instantiate(gameObject, gameObject.transform.parent);
            slotToClone.name = ("");
            heldItem = slotToClone.GetComponent<ObjectMovementScript>();
            heldItem.currentWedgeSize = newSize;
            heldItem.transform.position = this.transform.position;
            wantsToBeHeld = heldItem;
            heldItem.controlTaken = this;

            if (heldItem.objectType == 1)
            {
                float currentCount = CustomerRequestController.swissCount.x;
                currentCount = currentCount + 1;
                heldItem.name = ("Swiss" + currentCount);

            }
            else if (heldItem.objectType == 2)
            {
                float currentCount = CustomerRequestController.brieCount.x;
                currentCount = currentCount + 1;
                heldItem.name = ("Brie" + currentCount);
            }

        }
    }

    // requested by cutting board
    public float GiveCheese()
    {
        Debug.Log("floatSent");
        if (objectType == 24 && heldItem != null)
        {
            return toBeCut;
        }
        else
        { 
            return 1; 
        }
    }

    // Called by cut script
    public void setCutSize(float forMe,float forYou)
    {
        Debug.Log("Data Recieved: " + forMe + " | " + forYou); ;
        if (forMe != 1 || forYou != 1)
        {
            Debug.Log("Sent to Board");
            toBeCut = forYou;
            heldItem.currentWedgeSize = forMe;
            controllerCut.giveDataToClone();
        }

    }
}
