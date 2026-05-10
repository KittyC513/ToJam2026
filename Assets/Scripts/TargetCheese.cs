using UnityEngine;

public class TargetCheese : MonoBehaviour
{
    public float cheeseSize;
    public bool trashPenalty;
    public float minThreshold;
    public float maxCheeseWeight = 1;
    public float cheeseWeight;
    public bool isValid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isSelected)
        {
            CheckValidPiece(GameManager.Instance.requiredCutAmount);
        }
    }

    public void Weigh()
    {
        cheeseSize = GameManager.Instance.cheeseList[0].cheeseSize;
        cheeseWeight = maxCheeseWeight * cheeseSize;
        Debug.Log("Cheese weight: " + cheeseWeight);
    }

    public void CheckValidPiece(float validAmount)
    {
        Weigh();

        if (validAmount < minThreshold || validAmount > cheeseWeight)
        {
            isValid = false;
            Debug.Log("Invalid piece");
        }
        else
        {
            isValid = true;
            Debug.Log("Valid piece");
        }
    }

}
