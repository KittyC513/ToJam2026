using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;
using System;
using System.Linq;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class CustomerRequestController : MonoBehaviour
{

    public GameObject swissPrefab;
    public GameObject briePrefab;

    // x = success / y = failure 

    [SerializeField]
    private Vector2 score = new Vector2(0,0);

    // x = cheese type / y = cheese weight

    public Vector2 currentRequest;

    public static float leeway = 10f;

    public static Vector2 swissCount;

    public static Vector2 brieCount;

    [SerializeField]
    // x = min / y = max
    public Vector2 brieLimits;
    [SerializeField]
    // x = min / y = max
    public Vector2 swissLimits;

    // CHANGE BACK TO .8
    static public float refreashWheelThreshold = .8f;

    [SerializeField]
    public bool requestedCheese;

    private float currentSwissSize;

    private float currentBrieSize;

    private float lastSwissSize;

    private float lastBrieSize;

    public GameObject brieWheel;
    public GameObject swissWheel;

    public SlotScript[] brieSlots;
    public SlotScript[] swissSlots;
    public SlotScript[] otherSlots;

    void Start()
    {
        currentBrieSize = CalculateBrieSize();
        lastBrieSize = currentBrieSize;

        currentSwissSize = CalculateSwissSize();
        lastSwissSize = currentSwissSize;

        NewRequestTest();
        //       CalculateBrieSize();
        Debug.Log("Calculated Swiss: " + CalculateSwissSize());
    }

    void Update()
    {

        if (lastSwissSize != currentSwissSize)
        {
            lastSwissSize = currentSwissSize;


        }
        else if (lastBrieSize != currentBrieSize)
        {
            lastBrieSize = currentBrieSize;

        }

    }

    void NewRequestTest()
    {
        float coinFlip = Random.value;

        if (coinFlip > .5f) 
        { 
            // Swiss
            requestedCheese = true;
        }
        else
        { 
            // Brie
            requestedCheese= false;
        }

        if (requestedCheese)
        {
            currentRequest = new Vector2(1, Random.Range(swissLimits.x, (swissWheel.GetComponent<TempCheese>().tempCheeseWeight * CalculateSwissSize())));
        }
        else if (!requestedCheese)
        {
            currentRequest = new Vector2(2, Random.Range(brieLimits.x, (brieWheel.GetComponent<TempCheese>().tempCheeseWeight * CalculateBrieSize())));
        }

        Debug.Log("Type: " + currentRequest.x + " Weight: " + currentRequest.y);

    }

    private float CalculateBrieSize()
    {
        currentBrieSize = 1;

        ObjectMovementScript[] cheeseWheelArray = Array.Empty<ObjectMovementScript>();
        cheeseWheelArray = brieWheel.GetComponentsInChildren<ObjectMovementScript>();

        for (int i = 0; i < cheeseWheelArray.Length; i++) 
        {
            currentBrieSize = currentBrieSize - cheeseWheelArray[i].currentWedgeSize;
        }
        Debug.Log("Current Brie Sold: " +currentBrieSize);

        return currentBrieSize;
    }

    private float CalculateSwissSize()
    {
        currentSwissSize = 1;

        ObjectMovementScript[] cheeseWheelArray = Array.Empty<ObjectMovementScript>();
        cheeseWheelArray = swissWheel.GetComponentsInChildren<ObjectMovementScript>();

        for (int i = 0; i < cheeseWheelArray.Length; i++)
        {
            currentSwissSize = currentSwissSize - cheeseWheelArray[i].currentWedgeSize;
        }
        Debug.Log("Current Swiss Sold: " + currentSwissSize);

        return currentSwissSize;

    }

    public void OrderResult(int result)
    {
        Debug.Log("Result Ran");


        if (result == 1)
        {
//            Right Answer
            score.x = score.x + 1;
        }
        else if (result == 2)
        {
//            Wrong answer
            score.y = score.y + 1;
        }
        
    }

    public void RefreashWheelCheck()
    {
        if (otherSlots != null)
        {
            for (int i = 0; i < otherSlots.Length; i++)
            {
                if (otherSlots[i].heldItem != null && currentRequest.x == 1)
                {
                    if (otherSlots[i].heldItem.objectType == 1)
                    {
                        otherSlots[i].ResetSlot();
                    }
                }
                if (otherSlots[i].heldItem != null && currentRequest.x == 2)
                {
                    if (otherSlots[i].heldItem.objectType == 2)
                    {
                        otherSlots[i].ResetSlot();
                    }
                }
            }
        }

        if (currentRequest.x == 1 && CalculateSwissSize() > refreashWheelThreshold)
        {
            Debug.Log("Destroyed children swiss");
            swissCount.y += 1;

            int nbChildren = swissWheel.transform.childCount;

            for (int i = nbChildren - 1; i >= 0; i--)
            {
                DestroyImmediate(swissWheel.transform.GetChild(i).gameObject);
            }

            if (swissSlots != null)
            {
                for (int i = 0;i < swissSlots.Length; i++)
                {
                    swissSlots[i].ResetSlot();

                }

                GameObject newCheese = Instantiate<GameObject>(swissPrefab, swissWheel.transform);
                newCheese.transform.position = new Vector3(swissSlots[0].transform.position.x, swissSlots[0].transform.position.y, -3);
                swissSlots[0].heldItem = newCheese.GetComponent<ObjectMovementScript>();
                swissSlots[0].wantsToBeHeld = swissSlots[0].heldItem;
                swissSlots[0].heldItem.controlTaken = swissSlots[0];
                swissSlots[0].transform.gameObject.name = ("Swiss" + swissCount.x);
                swissSlots[0].heldItem.beingSlotted = true;
                swissSlots[0].heldItem.submitToTaker = true;

                swissWheel.GetComponent<TempCheese>().tempCheeseWeight = Random.Range(swissLimits.x,  swissLimits.y);

            }

        }
        else if (currentRequest.x == 2 && CalculateBrieSize() > refreashWheelThreshold)
        {
            Debug.Log("Destroyed children brie");
            brieCount.y += 1;

            int nbChildren = brieWheel.transform.childCount;

            for (int i = nbChildren - 1; i >= 0; i--)
            {
                DestroyImmediate(brieWheel.transform.GetChild(i).gameObject);
            }

            if (brieSlots != null)
            {
                for (int i = 0; i < brieSlots.Length; i++)
                {
                    brieSlots[i].ResetSlot();
                }

                GameObject newCheese = Instantiate<GameObject>(briePrefab, brieWheel.transform);
                newCheese.transform.position = new Vector3(brieSlots[0].transform.position.x, brieSlots[0].transform.position.y, -3);
                brieSlots[0].heldItem = newCheese.GetComponent<ObjectMovementScript>();
                brieSlots[0].wantsToBeHeld = brieSlots[0].heldItem;
                brieSlots[0].heldItem.controlTaken = brieSlots[0];
                brieSlots[0].transform.gameObject.name = ("Brie" + brieCount.x);
                brieSlots[0].heldItem.beingSlotted = true;
                brieSlots[0].heldItem.submitToTaker = true;

                brieWheel.GetComponent<TempCheese>().tempCheeseWeight = Random.Range(brieLimits.x, brieLimits.y);
            }
        }


        NewRequestTest();

    }

}
